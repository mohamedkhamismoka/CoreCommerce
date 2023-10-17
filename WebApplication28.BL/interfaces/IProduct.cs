using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication28.DAL.Entities;

namespace WebApplication28.BL.interfaces
{
    public interface IProduct
    {

        public IEnumerable<Product> getAll();
        public Product getById(int id);

        public void create(Product product);


        public void update(Product product);


        public void delete(int id);

        public void decrement(List<int> id, List<int> quantity);

        public void increment(List<int> id, List<int> quantity);




    }
}
