using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementDomain.IRepository.Base
{
    public interface IRepository<T> where T:class
    {
        Task<IEnumerable<T>> GetAllAsync(string storedProcedure, DynamicParameters parameters = null);
    }
}
