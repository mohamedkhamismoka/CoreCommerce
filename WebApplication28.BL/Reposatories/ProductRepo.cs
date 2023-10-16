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

 


    public class ProductRepo : IProduct
    {
        private readonly DatabaseContext db;

        public ProductRepo(DatabaseContext db)
        {
               this.db = db;
        }
        public void create(Product product)
        {
           db.Products.Add(product);
            db.SaveChanges();
        }

        public void delete(int id)
        {
            var result=db.Products.Find(id);
            db.Products.Remove(result);
            db.SaveChanges();
        }

        public IEnumerable<Product> getAll()
        {
            return this.db.Products.Select(a => a);
        }

        public Product getById(int id)
        {
            var result = db.Products.Find(id);
            return result;
        }

        public void update(Product product)
        {
            db.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
        public void decrement(List<int> id, List<int> quantity)
        {
            for (int i = 0; i < id.Count; i++)
            {
                var ele = db.Products.Find(id[i]);
                ele.quantity = ele.quantity - quantity[i];
            }

            db.SaveChanges();
        }
    }
}
