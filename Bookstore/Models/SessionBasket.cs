using Bookstore.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class SessionBasket : Basket //this gives eveyrthing from the basket class (see Basket.cs)
    {
        public static Basket GetBasket (IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            SessionBasket basket = session?.GetJson<SessionBasket>("Basket") ?? new SessionBasket(); //create instanc eof SessionBasket called basket

            basket.Session = session;

            return basket;
        }                       //is the session there? If not, make a new session 
        [JsonIgnore] //added the using
        public ISession Session { get; set; } //selected the second one down to get using

        public override void AddItem(Book bk, int qty) //this is from parent class "Basket"
        {
            base.AddItem(bk, qty);
            Session.SetJson("Basket", this); //see the infrastructure SessoinExtensions file set json
                //this keyword refers to the current instance of the class
        }

        public override void RemoveItem(Book bk)
        {
            base.RemoveItem(bk);
            Session.SetJson("Basket", this);
        }

        public override void ClearBasket()
        {
            base.ClearBasket();
            Session.Remove("Basket");
        }
    }
}
