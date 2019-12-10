using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.Facades;
using Web.Models;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        public OrderFacade OrderFacade { get; set; }
        public CompanyFacade CompanyFacade { get; set; }
        public orderItemItemFacade OrderItemItemFacade { get; set; }

        // GET: Order
        public async Task<ActionResult> Order()
        {
            List<OrderDto> orders = await CompanyFacade.GetAllOrders(User.Identity.Name, DateTime.Today);
            orders = orders.OrderBy(x => x.OrderStartTime).ToList();
            return View(orders);
        }

        public ActionResult NewOrder()
        {
            List<MenuItemDto> menuItems = new List<MenuItemDto>(){new MenuItemDto(){Name = "beer", SellPrice =  123}, new MenuItemDto(){Name = "vodka", SellPrice = 456}};

            var list = new List<SelectListItem>();
            foreach (var item in menuItems)
            {
                var x = new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                };
                list.Add(x);
            }

            //ViewBag.datasource = menuItems;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(OrderDto order)
        {
            order.OrderStartTime = DateTime.Now;
            
            await CompanyFacade.CreateNewOrderForCompany(User.Identity.Name, order);
            List<OrderDto> orders = await CompanyFacade.GetAllOrders(User.Identity.Name, DateTime.Today);
            orders = orders.OrderBy(x => x.OrderStartTime).ToList();
            return View("Order", orders);
        }

        public async Task<ActionResult> Details(int id)
        {
            var res = await OrderFacade.GetAsyncWithDependencies(id);
            return View("Details", res);
        }

        public async Task<ActionResult> Add(int id)
        {
            var menuItems = await CompanyFacade.GetAllMenuItems(User.Identity.Name);
            return View("Add", new NewItemToOrder
            {
                Items = menuItems,
                OrderId = id
            });
        }

        [HttpPost]
        public async Task<ActionResult> SaveItem(NewItemToOrder order)
        {
            var items = await CompanyFacade.GetAllMenuItems(User.Identity.Name);
            await OrderItemItemFacade.Create(new OrderItemDto
            {
                MenuItemId = items[order.SelectedItem].Id,
                IsPaid = false,
                OrderId = order.OrderId
            });

            List<OrderDto> orders = await CompanyFacade.GetAllOrders(User.Identity.Name, DateTime.Today);
            var res = await OrderFacade.GetAsyncWithDependencies(order.OrderId);
            return View("Details", res);
        }

        [HttpPost]
        public ActionResult Finish()
        {
            OrderFacade.CloseTheDay(User.Identity.Name, DateTime.Today);
            return View("Order");
        }

        public async Task<ActionResult> Pay(int id, int orderId)
        {
            var orderItem = await OrderItemItemFacade.GetAsync(id);
            orderItem.IsPaid = true;
            await OrderItemItemFacade.Update(orderItem);

            var res = await OrderFacade.GetAsyncWithDependencies(orderId);
            return View("Details", res);
        }
    }
}