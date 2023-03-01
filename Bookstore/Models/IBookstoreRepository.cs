using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public interface IBookstoreRepository
    {
        IQueryable<Book> Books { get; } //can read, but not write; the //<Book> is referring to the Model name "Book"
    }
}
//interafce is not a class but is a template for a class; it's an abstract class