using System.Data;
using Microsoft.Data.SqlClient;            // ✔ Driver moderno recomendado
using Microsoft.Extensions.Configuration;

namespace FinanblueBackend.Data
{
    public class DbContextDapper
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public DbContextDapper(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
