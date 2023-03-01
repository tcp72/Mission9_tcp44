using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.ViewModels
{
    public class BooksViewModel
    {
        public IQueryable<Book> Books { get; set; }//make iqueryable data type. Instance of "Book" model, named "Books"
        public PageInfo PageInfo { get; set; }
    }
}
