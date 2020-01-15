using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RestaurantManager.BusinessLayer.Facades;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "Owner")]
    public class StatisticsController : Controller
    {
        public CompanyFacade CompanyFacade { get; set; }
        // GET: Statistics
        public async Task<ActionResult> Statistics()
        {
            var orders = await CompanyFacade.GetAllOrdersWithDependencies(User.Identity.Name);
            StatisticsModel model = new StatisticsModel();
            
            orders.ForEach(o => o.Items.ForEach(i =>
            {
                string itemName = i.MenuItem.Name;
                int orderSellPrice = i.MenuItem.SellPrice * i.MenuItem.Amount;
                int orderBuyPrice = i.MenuItem.BuyPrice * i.MenuItem.Amount;

                if (!model.ItemsSellOverall.ContainsKey(itemName))
                {
                    model.ItemsSellOverall.Add(itemName, 0);
                }

                model.ItemsSellOverall[itemName] += i.MenuItem.Amount;
                model.CostsAll += orderSellPrice;
                model.RevenuesAll += orderBuyPrice;

                if (o.OrderStartTime > DateTime.Now.AddMonths(-1))
                {
                    if (!model.ItemsSellLastMonth.ContainsKey(itemName))
                    {
                        model.ItemsSellLastMonth.Add(itemName, 0);
                    }

                    model.ItemsSellLastMonth[itemName] += i.MenuItem.Amount;

                    model.CostsMonth += orderSellPrice;
                    model.RevenuesMonth += orderBuyPrice;
                }

                if (o.OrderStartTime > DateTime.Today)
                {
                    if (!model.ItemsSellLastToday.ContainsKey(itemName))
                    {
                        model.ItemsSellLastToday.Add(itemName, 0);
                    }

                    model.ItemsSellLastToday[itemName] += i.MenuItem.Amount;
                    
                    model.CostsToday += orderSellPrice;
                    model.RevenuesToday += orderBuyPrice;
                }
            }));

            return View("Statistics", model);
        }

        public async Task<ActionResult> DrawTodayChart()
        {
            var orders = await CompanyFacade.GetAllOrdersWithDependencies(User.Identity.Name);
            var itemsSell = new Dictionary<string, int>();

            (await CompanyFacade.GetAllMenuItems(User.Identity.Name)).ForEach(i => itemsSell.Add(i.Name, 0));
            orders.FindAll(o => o.OrderStartTime > DateTime.Today).ForEach(o => o.Items.ForEach(i => itemsSell[i.MenuItem.Name] += i.MenuItem.Amount));

            return PartialView("TodayChart", itemsSell);
        }

        public async Task<ActionResult> DrawOverallChart()
        {
            var orders = await CompanyFacade.GetAllOrdersWithDependencies(User.Identity.Name);
            var itemsSell = new Dictionary<string, int>();

            (await CompanyFacade.GetAllMenuItems(User.Identity.Name)).ForEach(i => itemsSell.Add(i.Name, 0));
            //orders.ForEach(o => o.Items.ForEach(i => itemsSell[i.MenuItem.Name] += i.MenuItem.Amount));
            var orderItems = orders.SelectMany(o => o.Items).ToList();

            foreach (var itemName in itemsSell.Keys.ToList())
            {
                itemsSell[itemName] = orderItems.FindAll(o => o.MenuItem.Name.Equals(itemName)).Count;
            }

            return PartialView("AllChart", itemsSell);
        }

        public async Task<ActionResult> DrawMonthChart()
        {
            var orders = await CompanyFacade.GetAllOrdersWithDependencies(User.Identity.Name);
            var itemsSell = new Dictionary<string, int>();

            (await CompanyFacade.GetAllMenuItems(User.Identity.Name)).ForEach(i => itemsSell.Add(i.Name, 0));
            orders.FindAll(o => o.OrderStartTime > DateTime.Now.AddMonths(-1)).ForEach(o => o.Items.ForEach(i => itemsSell[i.MenuItem.Name] += i.MenuItem.Amount));

            return PartialView("MonthChart", itemsSell);
        }

    }
}