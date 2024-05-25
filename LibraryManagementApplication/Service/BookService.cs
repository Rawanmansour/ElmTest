using Azure;
using Dapper;
using LibraryManagementDomain.DTO;
using LibraryManagementDomain.Entities;
using LibraryManagementDomain.IRepository.Base;
using LibraryManagementDomain.IService;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementApplication.Service
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _repository;
        public BookService(IRepository<Book> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Book>> GetAllAsync(BookSearchDTO param)
        {
            var parameters = new DynamicParameters();
            // Add any required parameters for the stored procedure
            parameters.Add("PageNumber", param.Page);
            parameters.Add("PageSize",param.PageSize);
            var deepSearch = false;
            if (!string.IsNullOrWhiteSpace(param.BookTitle))
            {
                parameters.Add("Title", param.BookTitle);
                deepSearch = true;
            }
            if (!string.IsNullOrWhiteSpace(param.Author))
            {
                parameters.Add("Author", param.Author);
                deepSearch = true;
            }
            if (!string.IsNullOrWhiteSpace(param.BookDescription))
            {
                parameters.Add("Description", param.BookDescription);
                deepSearch = true;
            }
            if (!string.IsNullOrWhiteSpace(param.PublishDate))
            {
                parameters.Add("PublishDate", param.PublishDate);
                deepSearch = true;
            }
            IEnumerable<Book> Result = new List<Book>();

            if (deepSearch)
            {
                Result = await _repository.GetAllAsync("SearchIndex", parameters);
            }
            else
            {
                Result = await _repository.GetAllAsync("Search", parameters);
            }
            // Deserialize JSON and map to BookInfo objects
            foreach (var item in Result)
            {
                var bookInfo = JsonConvert.DeserializeObject<BookInfo>(item.BookInfoJson);
                item.BookInfo = bookInfo;
                item.BookInfoJson = "";
            }
            return Result;
        }
    }
}
