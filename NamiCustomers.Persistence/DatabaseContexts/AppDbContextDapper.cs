
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace NamiCustomers.Persistence.DatabaseContexts
{
    public class AppDbContextDapper
    {

        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public AppDbContextDapper(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SAMPLEConnection");
        }

        public DbConnection GetDbconnection() => new SqlConnection(_connectionString);
    }
}
