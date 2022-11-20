using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication28.DAL.Entities;

namespace WebApplication28.BL.VM
{
    public class OrderVM
    {

        public OrderVM()
        {
            Date = DateTime.Now.Date;
        }
        public int Id { get; set; }
        public string userID { get; set; }

        public ApplicationUser User { get; set; }

    
        public int TotalPrice { get; set; }

        public IEnumerable<Product_order> product_order { get; set; }


        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "Invalid Mail")]
        public string mail { get; set; }

        [Required(ErrorMessage = "name Required")]
        [MinLength(10, ErrorMessage = "Min length = 10")]
        public string name { get; set; }

        [Required(ErrorMessage = "Address Required")]
        [RegularExpression("[0-9]{2,5}-[a-zA-Z]{1,50}-[a-zA-Z]{1,50}-[a-zA-Z]{1,50}", ErrorMessage = "address must like 12-Streetname-cityname-countryname")]

        public string Address { get; set; }
        [Required(ErrorMessage = "City Required")]
        [MinLength(5, ErrorMessage = "Min length = 5")]
        public string City { get; set; }
        [Required(ErrorMessage = "Phone Required")]
        [Phone(ErrorMessage = "Enter valid phone")]
        public string phone { get; set; }
     
        public DateTime Date { get; set; }


        [Required(ErrorMessage = "Name on Credit Required")]
        [MinLength(10, ErrorMessage = "Min length = 10")]
        public string creditname { get; set; }

        [Required(ErrorMessage = "Credit number Required")]
        public string Creditnumber { get; set; }
    }
}
