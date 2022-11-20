using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication28.BL.interfaces;
using WebApplication28.BL.VM;
using WebApplication28.DAL.Entities;

namespace WebApplication28.Areas.Client.Controllers
{

    [Area("Client")]
    public class CheckoutController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProduct product;
        private readonly IProductOrder product_Order;
        private readonly UserManager<ApplicationUser> usermanager;
        private readonly ICart cart;
        private readonly IProductCart prod_Cart;
        private readonly IOrder _order;

        public CheckoutController(IMapper mapper, IProduct product, IProductOrder product_Order,UserManager<ApplicationUser> usermanager, ICart cart, IProductCart prod_cart,IOrder _order)
        {
            this.mapper = mapper;
            this.product = product;
            this.product_Order = product_Order;
            this.usermanager = usermanager;
            this.cart = cart;
           this. prod_Cart = prod_cart;
            this._order = _order;
        }
        public IActionResult Index(int cartid)
        {
            ViewBag.count = prod_Cart.getUserProducts(cartid).Count();
            ViewBag.products=prod_Cart.getUserProducts(cartid).Where(a=>a.SaveForLater==false);
            ViewBag.cart = cartid;
            return View();
        }
        [HttpPost]
        public IActionResult Index(OrderVM order, int cartid)
        {

            List<int> product_identity = new List<int>();
            List<int> product_Quantity = new List<int>();
            List<Product_orderVM> product_OrderVMs = new List<Product_orderVM>();
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Order>(order);
                    var orderid = _order.create(data);
                    var products = prod_Cart.getUserProducts(cartid).Where(a => a.SaveForLater == false);
         
                    foreach (var item in products)
                    {
                        var ProductOrder = new Product_orderVM
                        {
                            OrderID = orderid,
                            ProductID = item.productID,
                            Quantity = item.quantity
                        };
                     product_OrderVMs.Add(ProductOrder);


                    }
                    var result = mapper.Map<IEnumerable<Product_order>>(product_OrderVMs);
                    product_Order.insert(result);

                    foreach (var item in products)
                    {
                        product_identity.Add(item.productID);
                        product_Quantity.Add(item.quantity);
                    }

                    product.decrement(product_identity, product_Quantity);
                    return RedirectToAction("Index", "Client", new { area = "Client",cartid=cartid });

                }
                ViewBag.count = prod_Cart.getUserProducts(cartid).Count();
                ViewBag.products = prod_Cart.getUserProducts(cartid).Where(a => a.SaveForLater == false);
                ViewBag.cart = cartid;
                return View(order);
            }catch(Exception e)
            {
                ViewBag.count = prod_Cart.getUserProducts(cartid).Count();
                ViewBag.products = prod_Cart.getUserProducts(cartid).Where(a => a.SaveForLater == false);
                ViewBag.cart = cartid;
                return View(order);
            }
        }

    }
}
