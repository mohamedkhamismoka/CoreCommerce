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
    public class CartRepo:ICart
    {
        private readonly DatabaseContext db;

        public CartRepo(DatabaseContext db)
        {
            this.db = db;
        }
        public int Create()
        {
        Cart cart = new Cart();
            var ele=db.carts.Add(cart);
            db.SaveChanges();
            return ele.Entity.CartId;
               
        }

        public Cart GetCart(int id)
        {
            var cart = db.carts.Find(id);
            return  cart;
        }

        public void Update(Cart cart)
        {
            db.Entry(cart).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var result = db.carts.Find(id);
            db.carts.Remove(result);
            db.SaveChanges();
        }

        public IEnumerable<Cart> getAll()
        {
            return db.carts.Select(a => a);
        }

    }
}
