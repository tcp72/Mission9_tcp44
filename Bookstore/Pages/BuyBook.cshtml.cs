using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Infrastructure;
using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookstore.Pages
{
    public class BuyBookModel : PageModel
    {
        private IBookstoreRepository repo { get; set; } //had to add Using at top here; constructor
        public BuyBookModel (IBookstoreRepository temp) //constructor
        {
            repo = temp;
        }
        public Basket basket { get; set; }
        public string ReturnUrl { get; set; } //declare this so bc of scope, stays alive outside function
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/"; //return URL if given one, else, go to index page
            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
        }

        public IActionResult OnPost(int bookID, string returnUrl) //this is coming from the BookSummary
                                                //in "Shared" BookSUmmarey line 27 or so atModel
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookID);

            //Get from the session if have algo stored. Stored in type Basket, named "basket", else, new
            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
            basket.AddItem(b, 1); //book, quantity of 1. see line 13 of the basket

            HttpContext.Session.SetJson("basket", basket);
            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
