using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.Facades;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "Owner, Manager")]
    public class StockController : Controller
    {
        public StockItemFacade StockItemFacade { get; set; }
        // GET: Stock
        public ActionResult Stock()
        {
            List<StockItemDto> stockItems = StockItemFacade.GetAllStockItems(User.Identity.Name);

            return View("Stock", stockItems);
        }

        public ActionResult Edit(int id)
        {
            var item = StockItemFacade.GetAsync(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Save(StockItemDto item)
        {
            StockItemFacade.Update(item);
            return View("Stock");
        }
    }
}