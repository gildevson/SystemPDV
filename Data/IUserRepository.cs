using FinanblueBackend.Models;
using System;
using System.Collections.Generic;

public interface IUserRepository
{
    Usuario GetUserByEmail(string email);
    Usuario GetUserById(Guid id);
    List<string> GetPermissoes(Guid usuarioId);
    Guid CreateUser(Usuario user, string senhaHash, int permissaoPadraoId);
}
