using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication28.BL.interfaces;
using WebApplication28.BL.VM;

namespace WebApplication28.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrder order;
        private readonly IMapper mapper;

        public OrderController(IOrder order,IMapper mapper)
        {
            this.order = order;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            var data = order.getAll();
            var result = mapper.Map<IEnumerable<OrderVM>>(data);
            return View(result);
        }

        public IActionResult Details(int orderid)
        {
            var data = order.getbyid(orderid);
            var result = mapper.Map<OrderVM>(data);
            return View(result);
        }
    }
}
