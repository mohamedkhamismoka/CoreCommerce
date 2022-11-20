using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication28.DAL.Entities;

namespace WebApplication28.BL.VM
{
    public class Product_cartVM
    {

        public Product_cartVM()
        {
            SaveForLater = false;
            quantity = 1;
        }
        public Product product { get; set; }
        public Cart cart { get; set; }
        public int productID { get; set; }
        public int CartId { get; set; }

        public bool SaveForLater { get; set; }
        public int quantity { get; set; }
    }
}
