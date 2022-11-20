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
    public class OrderRepo : IOrder
    {
        private readonly DatabaseContext db;

        public OrderRepo(DatabaseContext db)
        {
            this.db = db;
        }
        public int create(Order order)
        {
           var data=db.Orders.Add(order);
            db.SaveChanges();
            return data.Entity.Id;
        }

        public void delete(int id)
        {
            db.Orders.Remove(db.Orders.Find(id));
            db.SaveChanges();
        }

        public IEnumerable<Order> getAll()
        {
            return db.Orders.Select(a => a);
        }

        public Order getbyid(int id)
        {
            return db.Orders.Find(id);
        }

        public void update(Order order)
        {
            db.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
