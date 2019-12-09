using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.Facades;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        public OrderFacade OrderFacade { get; set; }

        // GET: Order
        public ActionResult Order()
        {
            List<OrderDto> orders = OrderFacade.GetAllOrders(User.Identity.Name);
            return View(orders);
        }

        public ActionResult NewOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(OrderDto order)
        {
            OrderFacade.CreateNewOrder(User.Identity.Name, order);
            return View("Order");
        }
    }
}