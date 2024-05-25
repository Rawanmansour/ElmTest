using Dapper;
using LibraryManagementDomain.IRepository.Base;
using LibraryManagementInfrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementApplication.Repository.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ILibraryManagementConnection _context;

        public Repository(ILibraryManagementConnection context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string storedProcedure, DynamicParameters parameters = null)
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }


    }
}
