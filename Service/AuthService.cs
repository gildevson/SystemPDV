using BCrypt.Net;
using SistemaCaixa.Data;
using SistemaCaixa.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaCaixa.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public (bool sucesso, Usuario user, string mensagem, string token) Login(string email, string senha)
        {
            var user = _repo.GetUserByEmail(email);

            if (user == null)
                return (false, null, "Usuário não encontrado.", null);

            if (!BCrypt.Net.BCrypt.Verify(senha, user.SenhaHash))
                return (false, null, "Senha incorreta.", null);

            var permissoes = _repo.GetPermissoes(user.Id);

            var token = GerarToken(user, permissoes);

            return (true, user, "Login OK!", token);
        }

        private string GerarToken(Usuario user, List<string> permissoes)
        {
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            foreach (var p in permissoes)
                claims.Add(new Claim(ClaimTypes.Role, p));

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
