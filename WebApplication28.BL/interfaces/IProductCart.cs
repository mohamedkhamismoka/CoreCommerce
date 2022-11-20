using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication28.DAL.Entities;

namespace WebApplication28.BL.interfaces
{
    public interface IProductCart
    {

        public IEnumerable<Product_cart> getAll();

        public IEnumerable<int> GetAllProducts(int cartid);

        public void update(Product_cart product_Cart);

        public void delete(int cartid);

        public void insert(Product_cart product_Cart);  

        public bool is_Product_Exist(int productid, int? cartid);

        public int userCount(int cartid);
        public IEnumerable<Product_cart> getUserProducts(int cartid);

        public void changeQuantity(int quantity,int product,int cart);

        public void removeProduct(int productid, int cartid);

        public void ChangeState(int productid, int cartid);
    }
}
