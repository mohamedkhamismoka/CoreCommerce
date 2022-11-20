using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication28.DAL.Entities;

namespace WebApplication28.DAL.Database
{
    public class DatabaseContext:IdentityDbContext<ApplicationUser>


    {


        public DatabaseContext(DbContextOptions<DatabaseContext>opt):base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product_cart>().HasKey(a => new{ a.productID,a.CartId});
            builder.Entity<Product_order>().HasKey(a => new { a.ProductID, a.OrderID });
            builder.Entity<IdentityUserLogin<string>>().HasKey(a => a.UserId);
            builder.Entity<IdentityUserRole<string>>().HasKey(a => new { a.UserId, a.RoleId });
            builder.Entity<IdentityUserToken<string>>().HasNoKey();
        }

        public DbSet<Product> Products { get; set; }
  
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> carts { get; set; }

        public DbSet<Product_order> product_Orders { get; set; }

        public DbSet<Product_cart> product_Carts { get; set; }

    }
}
