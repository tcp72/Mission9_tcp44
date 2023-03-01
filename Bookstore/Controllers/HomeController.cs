using Bookstore.Models;
using Bookstore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        private IBookstoreRepository repo; //typed this, added "Using" to top
        
        public  HomeController (IBookstoreRepository temp)
        {
            repo = temp;
        }

        //private BookstoreContext context { get; set; } //moved this stuff to be done in the Repository instead (see video 9)
        //public HomeController (BookstoreContext temp)
        //{
        //    context = temp;
        //}
        //or could do it on one line with lambda public HomeController (BookstoreContext temp) => context = temp;
        //lambda is not very good for more than one line things
        public IActionResult Index(int pageNum=1) //passed in int and PageNum; default PageNum to 1 if no other assignment for it
        {                                    //note: don't name pageNum "page". It is a reserved word
      
            int pageSize = 10; //how many we want to display in each page

            var x = new BooksViewModel //see the "PageInfo" ViewModel folder to see where getting all
            {
                Books = repo.Books //was "context" instead of repo before changed to repository setup; then had .ToList(); removed and added SQL
                .OrderBy(b => b.Title) //order based on the Title of the book
                .Skip((pageNum - 1) * pageSize) //skips the first so many before taking
                .Take(pageSize), //only get X results total on that page

                PageInfo = new PageInfo
                {
                    TotalNumBooks = repo.Books.Count(),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };
                return View(x); //this x is the name of the variable creaed above
        }
        //or could say public IActionResult Index() => View();
    }
}
