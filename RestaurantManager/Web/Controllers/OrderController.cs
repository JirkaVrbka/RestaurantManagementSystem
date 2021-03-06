﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public OrderItemFacade OrderItemFacade { get; set; }
        public MenuItemFacade MenuItemFacade { get; set; }

        // GET: Order
        public async Task<ActionResult> Order()
        {
            List<OrderWithFullDependencyDto> orders = await CompanyFacade.GetAllOrdersWithDependencies(User.Identity.Name);
            List<OrderInfo> ordersInfo = new List<OrderInfo>();

            orders = orders.OrderBy(x => x.OrderStartTime).ToList();
            orders.ForEach(o => ordersInfo.Add(
                new OrderInfo
                {
                    Id = o.Id, 
                    OrderStartTime = o.OrderStartTime, 
                    OrderTable = o.OrderTable, 
                    isPaid = o.Items.TrueForAll(i => i.IsPaid),
                    isClosed = o.IsClosed
                }));
            return View(ordersInfo);
        }

        [HttpPost]
        public async Task<ActionResult> Create(OrderDto order)
        {
            order.OrderStartTime = DateTime.Now;
            order.IsClosed = false;
            
            await CompanyFacade.CreateNewOrderForCompany(User.Identity.Name, order);

            List<OrderDto> orders = await CompanyFacade.GetAllOrders(User.Identity.Name, DateTime.Today);
            int newlyCreatedOrderId = orders.Find(o => DateTime.Compare(o.OrderStartTime, order.OrderStartTime.AddMilliseconds(-10)) > 0 && DateTime.Compare(o.OrderStartTime, order.OrderStartTime.AddMilliseconds(10)) < 0 && o.OrderTable == order.OrderTable).Id;

            return RedirectToAction("Detail", new { id = newlyCreatedOrderId });
        }

        public ActionResult NewOrder()
        {
            return View("NewOrder", new OrderDto());
        }

        public async Task<int> AddItemToOrder(int orderId, int itemId)
        {
            var item = await MenuItemFacade.GetAsync(itemId);

            await OrderItemFacade.Create(new OrderItemDto
            {
                MenuItemId = itemId,
                IsPaid = false,
                OrderId = orderId
            });

            var orderItems = await OrderItemFacade.GetByOrderId(orderId);
            var orderItemId = orderItems.Last().Id;

            item.Amount--;
            await MenuItemFacade.Update(item);
            return orderItemId;
        }
        
        public async Task<ActionResult> Finish()
        {
            await CompanyFacade.ClosePaidOrders(User.Identity.Name);
            return RedirectToAction("Order", "Order");
        }

        public async Task Pay(int orderItemId)
        {
            var orderItem = await OrderItemFacade.GetAsync(orderItemId);
            orderItem.IsPaid = true;
            await OrderItemFacade.Update(orderItem);
        }

        public async Task<ActionResult> Detail(int id)
        {
            var result = new OrderDetail
            {
                order = await OrderFacade.GetAsyncWithDependencies(id),
                menuItems = await CompanyFacade.GetAllMenuItems(User.Identity.Name)
            };
            return View("Detail", result);
        }
    }
}