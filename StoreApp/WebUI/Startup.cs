using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoreBL;
using StoreDL;
using Microsoft.EntityFrameworkCore;
//using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
                // Services are the things that your applciation is dependent on
        // Services have different lifetimes depending on when instances are created for that particular
        // dep:
        // 1. Transient = a new object is created per service call, lots of overhead 
        // 2. Scoped = a new instance is created per request 
        // 3. Singleton = an instance is shared for every request, leads to other requests waiting
        // used for dep injection
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<StoreAppDBContext>(options => options.UseNpgsql(parseElephantSQLURL(this.Configuration.GetConnectionString("StoreDB"))));
            services.AddScoped<IRepository, RepoDB>();
            services.AddScoped<ICustomerBL, CustomerBL>();
            services.AddScoped<ILocationBL, LocationBL>();
            services.AddScoped<IProductBL, ProductBL>();
            services.AddScoped<IInventoryBL, InventoryBL>();
        }
        // This method parse postgreSQL connection string from the appsettings.json
        public static string parseElephantSQLURL(string uriString)
        {
            //var uriString = connectionString; //ConfigurationManager.AppSettings["ELEPHANTSQL_URL"] ?? 'postgres://localhost/mydb;
            var uri = new Uri(uriString);
            var db = uri.AbsolutePath.Trim('/');
            var user = uri.UserInfo.Split(':')[0];
            var passwd = uri.UserInfo.Split(':')[1];
            var port = uri.Port > 0 ? uri.Port : 5432;
            var connStr = string.Format("Server={0};Database={1};User Id={2};Password={3};Port={4}",
                uri.Host, db, user, passwd, port);
            return connStr;
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
