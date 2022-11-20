using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication28.DAL.Entities
{
    public class Product_cart
    {
        public Product product { get; set; }    
        public Cart cart { get; set; }  
        public int productID { get; set; }
        public int CartId { get; set; }
        public bool SaveForLater { get; set; }

        public int quantity { get; set; }


    }
}
