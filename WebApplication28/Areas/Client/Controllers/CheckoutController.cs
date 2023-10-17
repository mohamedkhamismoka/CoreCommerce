using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication28.BL.interfaces;
using WebApplication28.BL.VM;
using WebApplication28.DAL.Database;
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
        private readonly DatabaseContext db;

        public CheckoutController(IMapper mapper, IProduct product, IProductOrder product_Order, DatabaseContext db, UserManager<ApplicationUser> usermanager, ICart cart, IProductCart prod_cart, IOrder _order)
        {
            this.mapper = mapper;
            this.product = product;
            this.product_Order = product_Order;
            this.usermanager = usermanager;
            this.cart = cart;
            this.prod_Cart = prod_cart;
            this._order = _order;
            this.db=db;
        }
        public IActionResult Index(int cartid)
        {
            ViewBag.count = prod_Cart.getUserProducts(cartid).Count();
            ViewBag.products = prod_Cart.getUserProducts(cartid).Where(a => a.SaveForLater == false);
            ViewBag.cart = cartid;
            return View();
        }
        [HttpPost]
        public IActionResult Index(OrderVM order, int cartid)
        {
            
                List<int> product_identity = new List<int>();
            List<int> product_Quantity = new List<int>();
            List<Product_orderVM> product_OrderVMs = new List<Product_orderVM>();
            var orderdata = 0;
           
                if (ModelState.IsValid)
                {
                var transaction = db.Database.BeginTransaction();
                try
                {
                   

                    var data = mapper.Map<Order>(order);

                    var orderid = _order.create(data);
                    orderdata = orderid;

                    var products = prod_Cart.getUserProducts(cartid).Where(a => a.SaveForLater == false);

                    foreach (var item in products)
                    {
                        var ProductOrder = new Product_orderVM
                        {
                            OrderID = orderdata,
                            ProductID = item.productID,
                            Quantity = item.quantity
                        };
                        product_OrderVMs.Add(ProductOrder);


                    }
                   
                    var result = mapper.Map<IEnumerable<Product_order>>(product_OrderVMs);
                    product_Order.Addrange(result);




                    foreach (var item in products)
                    {
                        product_identity.Add(item.productID);
                        product_Quantity.Add(item.quantity);
                    }
                   
                    product.decrement(product_identity, product_Quantity);
                    transaction.Commit();
                    return RedirectToAction("Index", "Client", new { area = "Client", cartid = cartid });

                }
                catch (Exception e)
                {
          
                    transaction.Rollback();
                    ViewBag.count = prod_Cart.getUserProducts(cartid).Count();
                    ViewBag.products = prod_Cart.getUserProducts(cartid).Where(a => a.SaveForLater == false);
                    ViewBag.cart = cartid;
                    return View(order);
                }


                }
                ViewBag.count = prod_Cart.getUserProducts(cartid).Count();
                ViewBag.products = prod_Cart.getUserProducts(cartid).Where(a => a.SaveForLater == false);
                ViewBag.cart = cartid;
                return View(order);

            }
            
        }
    }



