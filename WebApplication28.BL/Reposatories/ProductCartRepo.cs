using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication28.BL.interfaces;
using WebApplication28.BL.VM;
using WebApplication28.DAL.Database;
using WebApplication28.DAL.Entities;

namespace WebApplication28.BL.Reposatories
{
    public class ProductCartRepo : IProductCart
    {
        private readonly DatabaseContext db;
        private readonly UserManager<ApplicationUser> usermanager;
        private readonly IMapper map;

        public ProductCartRepo(DatabaseContext db,UserManager<ApplicationUser> usermanager,IMapper map)
        {
            this.db = db;
            this.usermanager = usermanager;
            this.map = map;
        }

        public void changeQuantity(int quantity, int product,int cart)
        {
         db.product_Carts.Find(product, cart).quantity = quantity;
            db.SaveChanges();
            

        }
        public void delete(int cartid)
        {
            var data = db.product_Carts.Select(a => a);
            List<Product_cart> product_Carts = new List<Product_cart>(); ;

            foreach (var item in data)
            {
                if (item.CartId == cartid)
                {
                    product_Carts.Add(item);
                }
            }
            db.product_Carts.RemoveRange(product_Carts);
            db.SaveChanges();
        }

        public IEnumerable<Product_cart> getAll()
        {
            return db.product_Carts.Select(a => a);
        }

        public IEnumerable<int> GetAllProducts(int cartid)
        {
            var data = db.product_Carts.Select(a => a);
            List<int> Product_ids = new List<int>();
            foreach (var item in data)
            {
                if (item.CartId == cartid)
                {
                    Product_ids.Add(item.productID);
                }


            }
            return Product_ids;
        }

        public void update(Product_cart product_Cart)
        {
            db.Entry(product_Cart).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }

        public void insert(Product_cart product_Cart)
        {
            db.product_Carts.Add(product_Cart);
            db.SaveChanges();
        }

        public bool is_Product_Exist(int productid, int ? cartid)
        {
            var data=db.product_Carts.Find(productid,cartid);
            return data!=null  ;
           
        }

        public int userCount(int cartid)
        {
            int count = db.product_Carts.Include(a => a.product).Where(a => a.CartId == cartid).Count();
            return count;
        }


        public IEnumerable<Product_cart> getUserProducts(int cartid)
        {
            return db.product_Carts.Include(a => a.product).Where(a => a.CartId == cartid);
        }

        public void removeProduct(int productid,int cartid)
        {
           var data=db.product_Carts.Where(a=>a.productID==productid &&a.CartId==cartid).FirstOrDefault();
            db.product_Carts.Remove(data);
            db.SaveChanges();
        }


        public void ChangeState(int productid, int cartid)
        {
            var data = db.product_Carts.Where(a => a.productID == productid && a.CartId == cartid).FirstOrDefault();
            data.SaveForLater = true;
            db.SaveChanges();

        }
    }
}
