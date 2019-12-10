﻿using System;
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
    [Authorize(Roles = "Owner, Manager")]
    public class MenuItemsController : Controller
    {
        public CompanyFacade CompanyFacade { get; set; }

        public MenuItemFacade MenuItemFacade { get; set; }

        // GET: Stock
        public async Task<ActionResult> MenuItems()
        {
            List<MenuItemDto> menuItems = await CompanyFacade.GetAllMenuItems(User.Identity.Name);

            return View("MenuItems", menuItems);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var item = await MenuItemFacade.GetAsync(id);
            return View("Edit", item);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(MenuItemDto item)
        {
            await MenuItemFacade.Update(item);
            return await MenuItems();
        }


        public async Task<ActionResult> Create()
        {
            return View(new MenuItemDto
            {
                Amount = 0,
                CompanyId = (await CompanyFacade.FindCompanyByEmployeeEmail(User.Identity.Name)).Id
            });
        }

        [HttpPost]
        public async Task<ActionResult> Create(MenuItemDto item)
        {
            await MenuItemFacade.Create(item);

            List<MenuItemDto> menuItems = await CompanyFacade.GetAllMenuItems(User.Identity.Name);

            return View("MenuItems", menuItems);
        }
    }
}