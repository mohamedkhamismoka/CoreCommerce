using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication28.BL.interfaces;
using WebApplication28.DAL.Database;
using WebApplication28.DAL.Entities;

namespace WebApplication28.BL.Reposatories
{
    public class ProductOrderRepo : IProductOrder
    {
        private readonly DatabaseContext db;

        public ProductOrderRepo(DatabaseContext db)
        {
            this.db = db;
        }
        public void delete(int orderid)
        {
            var data=db.product_Orders.Where(A => A.OrderID == orderid);
            db.product_Orders.RemoveRange(data);
            db.SaveChanges();
        }

        public IEnumerable<Product_order> get(int orderid)
        {
            return db.product_Orders.Where(a => a.OrderID == orderid);
        }

        public IEnumerable<Product_order> getAll()
        {
            return db.product_Orders.Select(a => a);
        }

        public void Addrange(IEnumerable<Product_order> product_Order)
        {
         db.product_Orders.AddRange(product_Order);
            db.SaveChanges();
        }

        public void update(Product_order product_order)
        {
            db.Entry(product_order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
