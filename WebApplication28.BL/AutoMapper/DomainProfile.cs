using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication28.BL.VM;
using WebApplication28.DAL.Entities;

namespace WebApplication28.BL.AutoMapper
{
    public class DomainProfile:Profile
    {
        public DomainProfile()
        {
            CreateMap<Product, ProductVM>();
            CreateMap<ProductVM, Product>();

            CreateMap<Cart, CartVM>();
            CreateMap<CartVM, Cart>();

            CreateMap<Order, OrderVM>();
            CreateMap<OrderVM, Order>();

       

            CreateMap<Product_order, Product_orderVM>();
            CreateMap<Product_orderVM, Product_order>();

            CreateMap<ApplicationUser, ApplictionUserVM>();

            CreateMap<ApplictionUserVM, ApplicationUser>();

            CreateMap<Product_cart, Product_cartVM>();

            CreateMap<Product_cartVM, Product_cart>();

        }
    }
}
