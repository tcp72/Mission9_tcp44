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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(); //corresponds to wwwroot

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultControllerRoute(); //this is a summarized version of what we had done before with name: default; pattern, etc.
                });
            });
        }
    }
}
//4 lines above was previously this, but it does the same thing with fewer lines of code
//endpoints.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{movieid?}");