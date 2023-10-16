using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication28.DAL.Entities
{
    public class Cart
    {
       
        public int CartId { get; set; } 
        public ApplicationUser user { get; set; }

        public IEnumerable<Product_cart> product_cart { get; set; }
    }
}
