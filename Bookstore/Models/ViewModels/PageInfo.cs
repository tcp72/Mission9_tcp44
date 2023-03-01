using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.ViewModels
{
    public class PageInfo
    {
        public int TotalNumBooks { get; set; }
        public int BooksPerPage { get; set; }
        public int CurrentPage { get; set; }

        //total number of pages; cast to double so works to get correct then cast all to int
        public int TotalPages => (int) Math.Ceiling((double)TotalNumBooks / BooksPerPage);
        //use (double) to cast next thing to double (like float), then cast all to (int) for display
        //use ceiling function to round up so always have new page even if just have 1 result on that
    }
}
