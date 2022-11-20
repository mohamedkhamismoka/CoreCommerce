using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication28.DAL.Entities;

namespace WebApplication28.BL.VM
{
    public class Product_orderVM
    {
        public int ProductID { get; set; }

        public int OrderID { get; set; }

        public int Quantity { get; set; }

        public Product product { get; set; }

        public Order order { get; set; }
    }
}
