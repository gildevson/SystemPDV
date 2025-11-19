using Dapper;
using FinanblueBackend.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace FinanblueBackend.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextDapper _dapper;

        public UserRepository(DbContextDapper dapper)
        {
            _dapper = dapper;
        }

        // ===============================
        // CRIAR USUÁRIO
        // ===============================
        public Guid CreateUser(Usuario user, string senhaHash, int permissaoPadraoId)
        {
            Guid newUserId = Guid.NewGuid();
            user.Id = newUserId;

            string sqlInsertUser =
                @"INSERT INTO Usuarios (Id, Nome, Email, SenhaHash, DataCriacao)
                  VALUES (@Id, @Nome, @Email, @SenhaHash, GETDATE())";

            string sqlInsertPermission =
                @"INSERT INTO UsuarioPermissao (UsuarioId, PermissaoId)
                  VALUES (@UsuarioId, @PermissaoId)";

            using IDbConnection connection = _dapper.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                connection.Execute(sqlInsertUser, new
                {
                    user.Id,
                    user.Nome,
                    user.Email,
                    SenhaHash = senhaHash
                }, transaction);

                connection.Execute(sqlInsertPermission, new
                {
                    UsuarioId = user.Id,
                    PermissaoId = permissaoPadraoId
                }, transaction);

                transaction.Commit();
                return newUserId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        // ===============================
        // LOGIN -> Buscar usuário por Email (CORRIGIDO)
        // ===============================
        public Usuario GetUserByEmail(string email)
        {
            string sql =
                @"SELECT Id, Nome, Email, SenhaHash 
                  FROM Usuarios 
                  WHERE LOWER(LTRIM(RTRIM(Email))) = LOWER(LTRIM(RTRIM(@Email)))";

            using IDbConnection connection = _dapper.CreateConnection();
            return connection.QueryFirstOrDefault<Usuario>(sql, new { Email = email });
        }

        // ===============================
        // Buscar permissões do usuário
        // ===============================
        public List<string> GetPermissoes(Guid usuarioId)
        {
            string sql = @"
                SELECT p.Nome
                FROM UsuarioPermissao up
                JOIN Permissoes p ON p.Id = up.PermissaoId
                WHERE up.UsuarioId = @UsuarioId
            ";

            using IDbConnection connection = _dapper.CreateConnection();
            return connection.Query<string>(sql, new { UsuarioId = usuarioId }).AsList();
        }

        // ===============================
        // Buscar usuário por ID (NECESSÁRIO)
        // ===============================
        public Usuario GetUserById(Guid id)
        {
            string sql = @"SELECT Id, Nome, Email 
                           FROM Usuarios 
                           WHERE Id = @Id";

            using IDbConnection connection = _dapper.CreateConnection();
            return connection.QueryFirstOrDefault<Usuario>(sql, new { Id = id });
        }
    }
}
