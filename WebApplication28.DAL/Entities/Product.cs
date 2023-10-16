using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebApplication28.DAL.Entities
{
    public class Product
    {
      
        public int productID { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        public string Description { get; set; }

        public int quantity { get; set; }

        public int unitPrice { get; set; }
        public string imgPath { get; set; }
        public IEnumerable<Product_cart> product_cart { get; set; }
        public IEnumerable<Product_order> product_order { get; set; }
    }
}
