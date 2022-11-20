using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication28.BL.VM
{
    public  class ApplictionUserVM
    {
        public string ID { get; set; }    
        [Required(ErrorMessage ="Email required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Address  Required")]

        [MinLength(5, ErrorMessage = "Min length is 5")]


        public string userName { get; set; }
        [Required(ErrorMessage = "Address  Required")]

        [MinLength(5, ErrorMessage = "Min length is 5")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Phone  Required")]

 

        public string PhoneNumber { get; set; }

     [Required(ErrorMessage ="passowrd is required")]

        public string password { get; set; }


        public string NewPssword { get; set; }
        [Required(ErrorMessage = "Please select Role")]
        public string RoleID { get; set; }


        public int Cartid { get; set; }
    }
}
