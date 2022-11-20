using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication28.DAL.Entities
{
    public class ApplicationUser :IdentityUser
    {
        public IEnumerable<Order> order { get; set; }
        public Cart cart { get; set; }

        public int? CartId { get; set; } 
        public string address { get; set; } 

        public bool isActive { get; set; }

       

    }
}
