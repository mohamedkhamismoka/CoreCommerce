using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication28.DAL.Entities;

namespace WebApplication28.BL.interfaces
{
    public interface IProductOrder
    {
        public void Addrange(IEnumerable<Product_order> product_Order);
        public void update(Product_order product_order);
        public void delete(int orderid);
        public IEnumerable<Product_order> get(int orderid);
        public IEnumerable<Product_order> getAll();
    }
}
