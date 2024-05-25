using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace LibraryManagementInfrastructure
{
    
        public class LibraryManagementConnection : ILibraryManagementConnection
        {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public LibraryManagementConnection(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ElmConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    }
    
}
