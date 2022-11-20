using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication28.BL.interfaces;
using WebApplication28.BL.VM;
using WebApplication28.DAL.Entities;

namespace WebApplication28.Areas.Client.Controllers
{

    [Area("Client")]
    public class ClientController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProduct product;
        private readonly UserManager<ApplicationUser> usermanager;
        private readonly ICart cart;
        private readonly IProductCart prod_Cart;

        public ClientController(IMapper mapper,IProduct product, UserManager<ApplicationUser> usermanager,ICart cart,IProductCart prod_cart)
        {
            this.mapper = mapper;
            this.product = product;
            this.usermanager = usermanager;
            this.cart = cart;
            prod_Cart = prod_cart;
        }
        public IActionResult Index(int cartid)
        {

            var data = product.getAll();
            var result = mapper.Map<IEnumerable<ProductVM>>(data);
            if (cartid == 0)
            {
                ViewBag.count = 0;
            }
            else
            {
                ViewBag.count = prod_Cart.userCount(cartid);
            }
           
            return View(result);
        }

        [HttpPost]
        public  async Task<JsonResult> Cart(string username)
        {
            var user = await usermanager.FindByNameAsync(username);
            if (user.CartId == null)
            {
                return Json(new { redirectToUrl = Url.Action("EmptyCart", "Client") });
            }
            else
            {
                return Json(new { redirectToUrl = Url.Action("UserCart", "Client", new { cartid = user.CartId}) });
            }
           
        }

        [HttpPost]
        public async Task<JsonResult> AddToCart(string username,int productid)
        {
            var user = await usermanager.FindByNameAsync(username);
            try
            {
                if (user.CartId == null)
                {
                    int cartid = cart.Create();
                    user.CartId = cartid;
                    Product_cartVM model = new Product_cartVM
                    {
                        CartId = cartid,
                        productID = productid




                    };
                    var count = 0;
                    var data = mapper.Map<Product_cart>(model);
                    prod_Cart.insert(data);
                    var products = prod_Cart.getUserProducts(cartid);
                    foreach (var item in products)
                    {
                        count++;
                    }
                    
                    return Json(new { message = "Product added Successfully to your cart" ,cartcount=count});


                }
                else
                {
                    int? cartid = user.CartId;
                    if (prod_Cart.is_Product_Exist(productid, cartid))
                    {
                        return Json(new { message = "Product Already in your cart" });
                    }
                    else
                    {
                       
                        Product_cartVM model = new Product_cartVM
                        {
                            CartId = (int)user.CartId,
                            productID = productid




                        };
                        var data = mapper.Map<Product_cart>(model);
                        prod_Cart.insert(data);
                        var count = 0;
                        var products = prod_Cart.getUserProducts((int)user.CartId);
                        foreach (var item in products)
                        {
                            count++;
                        }
                        return Json(new { message = "Product added Successfully to your cart", cartcount = count });
                    }

                }
            }catch(Exception e)
            {
                return Json(new { message = "some thing wrong" });
            }
          

        }

        public IActionResult EmptyCart()
        {
            return View();
        }

        public IActionResult UserCart(int cartid)
        {
            if (cartid == 0)
            {
                ViewBag.count = 0;
            }
            else
            {
                ViewBag.count = prod_Cart.userCount(cartid);
            }
            ViewBag.cartid = cartid;    
            var data = prod_Cart.getUserProducts(cartid);
            var result=mapper.Map<IEnumerable<Product_cartVM>>(data);
            return View(result);
        }

        [HttpPost]
        public JsonResult CheckProduct(int quantity, int productid, int cartid)
        {
          int product_quantity=  product.getById(productid).quantity;

            if(product_quantity >= quantity)
            {

                prod_Cart.changeQuantity(quantity, productid, cartid);

                return Json(new { status = "ok" });
            }
            else
            {
                return Json(new { status = "fail" });
            }
        }

        [HttpPost]
        public JsonResult RemoveFromCart( int productid, int cartid)
        {
            try
            {
               prod_Cart.removeProduct(productid,cartid);
                return Json(new { status = "ok" });
            }catch (Exception ex)
            {
                return Json(new { status = "fail" });
            }
        }


        [HttpPost]
        public JsonResult ChangeState(int productid, int cartid)
        {
            try
            {
                prod_Cart.ChangeState(productid, cartid);
                return Json(new { status = "ok" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "fail" });
            }
        }

    }
}
