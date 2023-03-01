using Bookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{//this class implements (like inheriting from) an instance of IBookstoreRepository
    public class EFBookstoreRepository : IBookstoreRepository
    {
        private BookstoreContext context { get; set; }
        public EFBookstoreRepository (BookstoreContext temp)
        {
            context = temp;
        }
        public IQueryable<Book> Books => context.Books;
    }
}
