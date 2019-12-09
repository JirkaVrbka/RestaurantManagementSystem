using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "Owner, Manager")]
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
            var asd = new ChangeAmountModel()
            {
                Amount = 5
            };

            return View(asd);
        }
    }
}