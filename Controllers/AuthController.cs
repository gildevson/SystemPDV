using Microsoft.AspNetCore.Mvc;
using SistemaCaixa.Data;
using SistemaCaixa.Models;
using SistemaCaixa.Service;

namespace SistemaCaixa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepo;

        public AuthController(IAuthService authService, IUserRepository userRepo)
        {
            _authService = authService;
            _userRepo = userRepo;
        }

        // ============================
        // LOGIN
        // ============================
        public class LoginRequest
        {
            public string Email { get; set; }
            public string Senha { get; set; }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var (sucesso, user, mensagem, token) =
                _authService.Login(request.Email, request.Senha);

            if (!sucesso)
                return Unauthorized(new { mensagem });

            return Ok(new
            {
                mensagem,
                token,
                usuario = new
                {
                    user.Id,
                    user.Nome,
                    user.Email
                }
            });
        }

        // ============================
        // REGISTER
        // ============================
        public class RegisterRequest
        {
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Senha { get; set; }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest dto)
        {
            try
            {
                // Verifica se o email já existe
                var existente = _userRepo.GetUserByEmail(dto.Email);
                if (existente != null)
                    return BadRequest("Email já está cadastrado.");

                // Gera hash
                string hash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);

                var user = new Usuario
                {
                    Nome = dto.Nome,
                    Email = dto.Email
                };

                // 2 = Permissão padrão (USUARIO)
                Guid idGerado = _userRepo.CreateUser(user, hash, permissaoPadraoId: 2);

                return Created("", new
                {
                    mensagem = "Usuário criado com sucesso",
                    userId = idGerado,
                    email = user.Email
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro no registro: {ex.Message}");
            }
        }

        // ============================
        // TESTE
        // ============================
        [HttpGet("ping")]
        public IActionResult Ping() => Ok("Auth funcionando!");
    }
}