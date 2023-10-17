using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication28.DAL.Entities
{
    public class Order
    {
      
        public int Id { get; set; }
        public string userID { get; set; }

        [ForeignKey("userID")]
        public ApplicationUser User { get; set; }

        [Column(TypeName ="Date")]
        
        public DateTime Date { get; set; }
     
        public decimal TotalPrice { get; set; }

        public IEnumerable<Product_order> product_order { get; set; }




       
        public string mail { get; set; }

        [StringLength(20)]
        public string name { get; set; }



        public string Address { get; set; }

        public string City { get; set; }
 
        public string phone { get; set; }



 
        public string creditname { get; set; }

        public string Creditnumber { get; set; }





    }
}
