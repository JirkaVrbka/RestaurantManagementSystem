﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.Facades;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "Owner, Manager")]
    public class StockController : Controller
    {
        public CompanyFacade CompanyFacade { get; set; }

        public MenuItemFacade MenuItemFacade { get; set; }

        // GET: Stock
        public async Task<ActionResult> Stock()
        {
            List<MenuItemDto> stockItems = await CompanyFacade.GetAllMenuItems(User.Identity.Name);

            return View("Stock", stockItems);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var item = await MenuItemFacade.GetAsync(id);
            return View(item);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(MenuItemDto item)
        {
            await MenuItemFacade.Update(item);
            return await Stock();
        }

        public ActionResult Create()
        {
            return View(new StockItemCreation());
        }

        [HttpPost]
        public async Task<ActionResult> Create(StockItemCreation item)
        {
            int companyId = (await CompanyFacade.FindCompanyByEmployeeEmail(User.Identity.Name)).Id;

            await MenuItemFacade.Create(new MenuItemDto
            {
                CompanyId = companyId,
                Name = item.MenuItem.Name,
                SellPrice = item.MenuItem.SellPrice,
                BuyPrice = item.MenuItem.BuyPrice,
                Amount = 0
            });

            return Stock().Result;
        }
    }
}