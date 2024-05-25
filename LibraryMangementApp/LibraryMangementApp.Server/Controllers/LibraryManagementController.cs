using LibraryManagementDomain.DTO;
using LibraryManagementDomain.Entities;
using LibraryManagementDomain.IService;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMangementApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibraryManagementController : ControllerBase
    {
        private readonly IBookService _bookService;
        public LibraryManagementController(IBookService bookService, ILogger<LibraryManagementController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<LibraryManagementController> _logger;


        [HttpPost(Name = "GetBooks")]
        public async Task<IEnumerable<Book>> Get([FromBody] BookSearchDTO obj)
        {
            return (await _bookService.GetAllAsync(obj)).ToArray();
           
        }
    }
}
