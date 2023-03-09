using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Components
{
    public class TypesViewComponent : ViewComponent //right click and bring in "using" here
    {                       //ViewComponent is built in
        private IBookstoreRepository repo { get; set; } //hover

        public TypesViewComponent (IBookstoreRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["bookCategory"]; //?nullible in case nothing there

            var types = repo.Books
                .Select(x => x.Category) //to sort by Category acho. In vids did by ProjectType
                .Distinct()
                .OrderBy(x => x);

            return View(types);
        }
    }
}
