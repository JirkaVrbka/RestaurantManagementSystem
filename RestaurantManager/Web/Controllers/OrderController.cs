using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.Facades;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        public OrderFacade OrderFacade { get; set; }
        public CompanyFacade CompanyFacade { get; set; }

        // GET: Order
        public async Task<ActionResult> Order()
        {
            List<OrderDto> orders = await CompanyFacade.GetAllOrders(User.Identity.Name);
            orders = orders.OrderBy(x => x.OrderStartTime).ToList();
            return View(orders);
        }

        public ActionResult NewOrder()
        {
            List<MenuItemDto> menuItems = new List<MenuItemDto>(){new MenuItemDto(){Name = "beer", Price = 123}, new MenuItemDto(){Name = "vodka", Price = 456}};
            ViewBag.datasource = menuItems;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(OrderDto order)
        {
            await CompanyFacade.CreateNewOrderForCompany(User.Identity.Name, order);
            return View("Order");
        }
    }
}