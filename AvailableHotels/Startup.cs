using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvailableHotels
{
    public class Startup
    {
        public static string currentPath;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            ///Gets the current app path
            currentPath = env.ContentRootPath;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Hotels/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                ///route when the API or a view is provided with parameters
                endpoints.MapControllerRoute(
                    name: "indexparams",
                pattern: "{controller=Hotels}/{action=Index}/{fromDate}/{toDate}/{city}/{numberOfAdults}");

                ///route when no parameters are specified
                endpoints.MapControllerRoute(
                        name: "default",
                pattern: "{controller=Hotels}/{action=Index}/{id?}");

            });
        }
    }
}
