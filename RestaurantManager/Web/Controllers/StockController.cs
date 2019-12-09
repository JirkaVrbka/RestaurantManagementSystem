using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantManager.BusinessLayer.DTOs.DTOs;

namespace Web.Controllers
{
    public class StockController : Controller
    {
        // GET: Stock
        public ActionResult Stock()
        {
            var itemsTest = new List<StockItemDto>()
            {
                new StockItemDto()
                {
                    Name = "Beer",
                    Amount = 5,
                    BuyPrice = 20
                }
            };

            return View("Stock", itemsTest);
        }

        public ActionResult ChangeAmountOfStockItem()
        {
            return View();
        }
    }
}