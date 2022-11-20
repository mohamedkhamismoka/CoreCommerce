using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication28.DAL.Entities;

namespace WebApplication28.BL.VM
{
    public  class ProductVM
    {
        public int productID { get; set; }

        [Required(ErrorMessage ="Product Name is required")]
        [MinLength(3,ErrorMessage ="name must be greater than 2 letters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MinLength(10, ErrorMessage = "Description must be greater than 9 letters")]
        public string Description { get; set; }



        [Required(ErrorMessage = "Quantity is required")]
         [Range(1,10000,ErrorMessage ="Quqntity must br greater than 0")]
        public int quantity { get; set; }

        [Required(ErrorMessage = "unitPrice is required")]
        [Range(1, 10000, ErrorMessage = "unitPrice must br greater than 0")]
        public int unitPrice { get; set; }


        public string imgPath { get; set; }
     
        public IFormFile img { get; set; }
        public IEnumerable<Product_cart> product_cart { get; set; }
        public IEnumerable<Product_order> product_order { get; set; }
    }
}
