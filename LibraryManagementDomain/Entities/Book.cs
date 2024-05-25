using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementDomain.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookInfoJson { get; set; }
        public BookInfo BookInfo { get; set; }
    }
}
