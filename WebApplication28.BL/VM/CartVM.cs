using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication28.DAL.Entities;

namespace WebApplication28.BL.VM
{
    public  class CartVM
    {
        public int CartId { get; set; }
        public ApplicationUser user { get; set; }

        public IEnumerable<Product_cart> product_cart { get; set; }
    }
}
