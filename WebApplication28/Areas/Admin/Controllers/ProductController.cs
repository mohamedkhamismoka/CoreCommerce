using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApplication28.BL.interfaces;
using WebApplication28.BL.services;
using WebApplication28.BL.VM;
using WebApplication28.DAL.Entities;

namespace WebApplication28.Areas.Admin.Controllers
{[Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProduct product;
        private readonly IMapper mapper;

        public ProductController(IProduct Product,IMapper mapper)
        {
            product = Product;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            var products = product.getAll();
            var data=mapper.Map<IEnumerable< ProductVM>>(products); 
            return View(data);
        }

        public IActionResult create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult create(ProductVM NewProduct)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (NewProduct.img != null) {

                        var imgname = imageUploader.upload(NewProduct.img);
                        var result = mapper.Map<Product>(NewProduct);
                        result.imgPath = imgname;
                        product.create(result);

                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        ModelState.AddModelError("", "please Choose Image ");
                        return View(NewProduct);
                    }
               
                 
                }
                return View(NewProduct);

            }
            catch (Exception e)
            {
                return View(NewProduct);
            }
  
        }

        public IActionResult Update(int id)
        {
            var ProductToUPdate = product.getById(id);
            var result = mapper.Map<ProductVM>(ProductToUPdate);
            return View(result);

        }

        public IActionResult details(int id)
        {
            var data=product.getById(id);
            var result = mapper.Map<ProductVM>(data);
                return  View(result);
        }

        [HttpPost]
        public IActionResult Update(ProductVM NewProduct,IFormFile newImg)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (newImg == null)
                    {
                       
                        var result = mapper.Map<Product>(NewProduct);
                    
                        product.update(result);

                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {

                        imageUploader.delete(NewProduct.imgPath);
                        var imgname = imageUploader.upload(newImg);
                        var result = mapper.Map<Product>(NewProduct);
                        result.imgPath = imgname;
                        product.update(result);

                        return RedirectToAction("Index", "Product");
                    }

                   

                }
                return View(NewProduct);

            }
            catch (Exception e)
            {
                return View(NewProduct);
            }

        }


        public IActionResult delete(int id)
        {
            var data = product.getById(id);
            var result = mapper.Map<ProductVM>(data);
            return View(result);
        }

        [HttpPost]
        [ActionName("delete")]
        public IActionResult Confirmdelete(int id)
        {
            
            product.delete(id);
            return RedirectToAction("Index", "Product");
        }
    }
}
