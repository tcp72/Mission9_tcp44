using Bookstore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-blah")] //look for div with attribute named "page-blah"
    public class PaginationTagHelper : TagHelper //inherit from overall TagHelper
    {
        //dynamically create page links

        private IUrlHelperFactory uhf; //click and resolve squiggle
        public PaginationTagHelper (IUrlHelperFactory temp) //make an instance of this class (built into C# or asp.net); helps build link
        {
            uhf = temp;
        }
        [ViewContext] //notate that is ViewContext
        [HtmlAttributeNotBound] //makes it so that 
        public ViewContext vc { get; set; }

        //different than View Context
        public PageInfo PageBlah { get; set; } //**THIS PageBlah is same idea as a class in HTML*** used to reference, etc.
        //above is going to receive from the "page-blah" class at the bottom of index.html page for pagination***
        public string PageAction { get; set; } //**important. Received in the class on index page

        //added these from bootstrap part of boook
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process (TagHelperContext thc, TagHelperOutput tho) //override = instead of using parent class portion, use this
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc); //get info about view that adding these tags and pass it the ViewContext file

            TagBuilder final = new TagBuilder("div"); //use this tag helper with a div (see top)

            for (int i = 1; i <= PageBlah.TotalPages; i++)
            {
                TagBuilder tb = new TagBuilder("a"); //this helps build tags; one for each page; display total number, one at a time
                tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i }); //PageAction, then what page going to
                tb.InnerHtml.Append(i.ToString());

                //added next 4 lines from bootstrap part of book
                if (PageClassesEnabled)
                {
                    tb.AddCssClass(PageClass);
                    tb.AddCssClass(i == PageBlah.CurrentPage
                        ? PageClassSelected : PageClassNormal);
                }


                    final.InnerHtml.AppendHtml(tb);
            }

            tho.Content.AppendHtml(final.InnerHtml);
        }
    }
}
