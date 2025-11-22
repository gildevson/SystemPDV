using Microsoft.AspNetCore.Mvc;
using SistemaCaixa.Models;
using System;
using System.Collections.Generic;

namespace SistemaCaixa.Data
{
    public interface IUserRepository
    {
        Guid CreateUser(Usuario user, string senhaHash, int permissaoPadraoId);
        Usuario GetUserByEmail(string email);
        Usuario GetUserById(Guid id);
        List<string> GetPermissoes(Guid usuarioId);
        IEnumerable<Usuario> GetAllUsers();



    }
}
