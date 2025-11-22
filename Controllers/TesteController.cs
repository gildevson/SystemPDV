
using BCrypt.Net;
using Dapper;
using SistemaCaixa.Data;
using Microsoft.AspNetCore.Mvc;
using SistemaCaixa.Data;

namespace SistemaCaixa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TesteController : ControllerBase
    {
        private readonly DbContextDapper _dapper;

        public TesteController(DbContextDapper dapper)
        {
            _dapper = dapper;
        }

        // ✅ 1️⃣ Testa conexão com o banco
        [HttpGet("conexao")]
        public IActionResult TestarConexao()
        {
            try
            {
                using var connection = _dapper.CreateConnection();
                connection.Open();
                return Ok("✅ Conexão com o banco de dados SQL Server bem-sucedida!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Erro ao conectar com o banco: {ex.Message}");
            }
        }

        // ✅ 2️⃣ Lista os 10 primeiros usuários
        [HttpGet("usuarios")]
        public IActionResult GetUsuarios()
        {
            using var connection = _dapper.CreateConnection();
            var usuarios = connection.Query("SELECT TOP 10 * FROM Usuarios");
            return Ok(usuarios);
        }

        // ✅ 3️⃣ Insere um novo usuário (fixo, para teste)
        [HttpPost("inserir")]
        public IActionResult InserirUsuario()
        {
            using var connection = _dapper.CreateConnection();

            var sql = @"INSERT INTO dbo.Usuarios (Nome, Email, SenhaHash)
                        VALUES (@Nome, @Email, @SenhaHash)";

            connection.Execute(sql, new
            {
                Nome = "Gilson Fonseca",
                Email = "gilson@teste.com",
                SenhaHash = BCrypt.Net.BCrypt.HashPassword("123456")
            });

            return Ok("✅ Usuário inserido com sucesso!");
        }
    }
}
