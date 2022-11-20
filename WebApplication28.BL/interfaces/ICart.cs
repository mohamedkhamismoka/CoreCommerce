using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication28.DAL.Entities;

namespace WebApplication28.BL.interfaces
{
    public interface ICart
    {

        public int Create();
        public Cart GetCart(int id);

        public void Update(Cart cart);  

        public void Delete(int id);

        public IEnumerable<Cart> getAll();
    }
}
