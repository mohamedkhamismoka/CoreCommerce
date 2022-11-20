using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication28.DAL.Entities;

namespace WebApplication28.BL.interfaces
{
    public interface IOrder
    {

        public int create(Order order);
        public void update(Order order);

           public void delete(int id);

        public IEnumerable<Order> getAll();
        public Order getbyid(int id);
    }
}
