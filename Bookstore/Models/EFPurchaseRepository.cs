using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class EFPurchaseRepository : IPurchaseRepository
    {
        private BookstoreContext context; //set up private instance of this context
        public EFPurchaseRepository (BookstoreContext temp)
        {
            context = temp; //set context = to the actual context variable. Now can use outside
        }
        public IQueryable<Purchase> Purchases => context.Purchase.Include(x => x.Lines).ThenInclude(x => x.Book); //in that context, Purchase table, item called "Lines" from Purchase Model
        //above get the entries from Purchase. Then book; so data has purchase and the details of the Book
        public void SavePurchase(Purchase purchase)
        {
            context.AttachRange(purchase.Lines.Select(x => x.Book));
            if (purchase.PurchaseId == 0)
            {
                context.Purchase.Add(purchase);
            }

            context.SaveChanges();
        }
    }
}
