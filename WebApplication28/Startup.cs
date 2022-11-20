using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication28.BL.AutoMapper;
using WebApplication28.BL.interfaces;
using WebApplication28.BL.Reposatories;
using WebApplication28.DAL.Database;
using WebApplication28.DAL.Entities;

namespace WebApplication28
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<IProduct, ProductRepo>();
            services.AddScoped<ICart, CartRepo>();
            services.AddScoped<IProductCart, ProductCartRepo>();
            services.AddScoped<IProductOrder, ProductOrderRepo>();
            services.AddScoped<IOrder, OrderRepo>();
            services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            
            services.AddControllersWithViews();
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<DatabaseContext>()
              

                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);
            services.AddDbContextPool<DatabaseContext>(opt =>  opt.UseSqlServer(Configuration.GetConnectionString("CoreCommerce")));
       



            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
           options =>
           {
               options.LoginPath = new PathString("/Admin/Admin/login");
               options.AccessDeniedPath = new PathString("/Admin/Admin/login");
           });





            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 1;

            });
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
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area=Client}/{controller=Client}/{action=Index}/{id?}"
                );
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

         

        }
    }
}
