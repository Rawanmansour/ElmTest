using LibraryManagementDomain.DTO;
using LibraryManagementDomain.Entities;
using LibraryManagementDomain.IRepository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementDomain.IService
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllAsync(BookSearchDTO param);
    }
}
