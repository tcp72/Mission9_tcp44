using Bookstore.Models;
using Bookstore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        
        public IConfiguration Configuration { get; set; }
        public Startup (IConfiguration temp)
        {
            Configuration = temp;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); //added this

            services.AddDbContext<BookstoreContext>(options =>
           {
               options.UseSqlite(Configuration["ConnectionStrings:BookstoreDBConnection"]); //this is from the startup and appsettings.json pages Connectionstring
            });

            services.AddScoped<IBookstoreRepository, EFBookstoreRepository>(); //had me add Using to top
            services.AddScoped<IPurchaseRepository, EFPurchaseRepository>();//when have ipurchrep, referring to efpurrep

            services.AddRazorPages(); //to be able to use razerpages

            services.AddDistributedMemoryCache();
            services.AddSession();

            //when see the <basket> passed in, will go get a new basket (remember SBasket is inherited and comes from SessioNBasket
            services.AddScoped<Basket>(x => SessionBasket.GetBasket(x));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //references SessionBasket
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(); //corresponds to wwwroot
            app.UseSession();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
       //**3 possibilities: either get category + page; just category; or just page, and still can get to the right page**
            {
                endpoints.MapControllerRoute("categorypage",
                    "{bookCategory}/Page{pageNum}",
                    new { Controller = "Home", action = "Index" }); //w/in {} means it is info that'll be passed in

                endpoints.MapControllerRoute( //endpoints are executed in order
                    name: "Paging", //note can remove "name", "pattern", "defaults" to look like other two
                    pattern: "page{pageNum}", //"PageNum" comes from HomeController; take inside {} and remove that from URL; before is what will display
                    defaults: new { Controller = "Home", action = "Index", pageNum = 1}); //default is page 1

                endpoints.MapControllerRoute("category",
                    "{bookCategory}",
                    new { Controller = "Home", action = "Index", pageNum = 1 });

                //(endpoints.MapControllerRoute
                //    ("category",
                //    "{bookCategory}",     //if that's all we receive is the "category"
                //    new { Controller = "Home", action = "Index", pageNum = 1});
                //if they just click on "category" then default to 1st page of that category

                endpoints.MapDefaultControllerRoute(); //this is a summarized version of what we had done before with name: default; pattern, etc.

                endpoints.MapRazorPages(); //add this for razor pages
            });
        }
    }
}
//4 lines above was previously this, but it does the same thing with fewer lines of code
//endpoints.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{movieid?}");