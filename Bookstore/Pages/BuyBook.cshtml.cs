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
        public BuyBookModel (IBookstoreRepository temp, Basket b) //constructor
        {
            repo = temp;
            basket = b;
        }
        public Basket basket { get; set; }
        public string ReturnUrl { get; set; } //declare this so bc of scope, stays alive outside function
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/"; //return URL if given one, else, go to index page
            //basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
            //this was to take care of session, but removed because now in Startup. See video 4
        }

        public IActionResult OnPost(int bookID, string returnUrl) //this is coming from the BookSummary
                                                //in "Shared" BookSUmmarey line 27 or so atModel
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookID == bookID);

            //Get from the session if have algo stored. Stored in type Basket, named "basket", else, new
            //basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();; now in Startup (see vid 4)
            basket.AddItem(b, 1); //book, quantity of 1. see line 13 of the basket

            HttpContext.Session.SetJson("basket", basket);
            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
        public IActionResult OnPostRemove(int bookId, string returnUrl)
        {
        //go to the RemoveItem method, find the Item that matches that Book id and pass to that method, which removes it
            basket.RemoveItem(basket.Items.First(x => x.Book.BookID == bookId).Book); //go to instance of session basket from above (19 or so) and call RemoveItem method

            return RedirectToPage (new {ReturnUrl = returnUrl});
        }
    }
}
