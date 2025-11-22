using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SistemaCaixa.Data
{
    public class DbContextDapper
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public DbContextDapper(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("ConnectionWork");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
