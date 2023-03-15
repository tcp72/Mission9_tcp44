using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class Basket
    {
        public List<BasketLineItem> Items { get; set; } = new List<BasketLineItem>();
        //called "Items". Declared with "Items"; Instantiate with new List< . . . (confirmed)

        public virtual void AddItem (Book bk, int qty) //alias for this item of Book called bk
            { //virtual allows this method to be overridden when we inherit from it
                BasketLineItem line = Items
                    .Where(b => b.Book.BookID == bk.BookID)
                    .FirstOrDefault(); //get whatever comes up first

                if (line == null)
                {
                    Items.Add(new BasketLineItem
                    {
                    Book = bk,
                    Quantity = qty
                    });
                }
                else
            {
                line.Quantity += qty;
            }
        }

        public virtual void RemoveItem(Book bk)
        {
            Items.RemoveAll(x => x.Book.BookID == bk.BookID);
            //look and see where Books in list match the ones passed in and remove those
        }

        public virtual void ClearBasket()
        {
            Items.Clear(); //gets rid of everything in the list from line 10
        }

        public Double CalculateTotal()
        {
            double sum = Items.Sum(x => x.Quantity * x.Book.Price); //this is saying that will pay 25 each time
            return sum;         //quantity * price for each individual. Sum gets each unit and sums it
        }
    }

    public class BasketLineItem
    {
        [Key]
        public int LineID { get; set; }
        public Book Book { get; set; } //which book they're buying. TYpe Book. Instantiated
        public int Quantity { get; set; } //how many of each book do they want
    }
}
