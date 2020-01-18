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
                int orderSellPrice = i.MenuItem.SellPrice;
                int orderBuyPrice = i.MenuItem.BuyPrice;

                if (!model.ItemsSellOverall.ContainsKey(itemName))
                {
                    model.ItemsSellOverall.Add(itemName, 0);
                }

                model.ItemsSellOverall[itemName] += i.MenuItem.Amount;
                model.CostsAll += orderBuyPrice;
                model.RevenuesAll += orderSellPrice;

                if (o.OrderStartTime > DateTime.Now.AddMonths(-1))
                {
                    if (!model.ItemsSellLastMonth.ContainsKey(itemName))
                    {
                        model.ItemsSellLastMonth.Add(itemName, 0);
                    }

                    model.ItemsSellLastMonth[itemName]++;

                    model.CostsMonth += orderBuyPrice;
                    model.RevenuesMonth += orderSellPrice;
                }

                if (o.OrderStartTime > DateTime.Today)
                {
                    if (!model.ItemsSellLastToday.ContainsKey(itemName))
                    {
                        model.ItemsSellLastToday.Add(itemName, 0);
                    }

                    model.ItemsSellLastToday[itemName]++;
                    
                    model.CostsToday += orderBuyPrice;
                    model.RevenuesToday += orderSellPrice;
                }
            }));

            return View("Statistics", model);
        }

        public async Task<ActionResult> DrawTodayChart()
        {
            var orders = await CompanyFacade.GetAllOrdersWithDependencies(User.Identity.Name);
            var itemsSell = new Dictionary<string, int>();

            (await CompanyFacade.GetAllMenuItems(User.Identity.Name)).ForEach(i => itemsSell.Add(i.Name, 0));
            orders.FindAll(o => o.OrderStartTime > DateTime.Today).ForEach(o => o.Items.ForEach(i => itemsSell[i.MenuItem.Name]++));

            return PartialView("TodayChart", itemsSell);
        }

        public async Task<ActionResult> DrawOverallChart()
        {
            var orders = await CompanyFacade.GetAllOrdersWithDependencies(User.Identity.Name);
            var itemsSell = new Dictionary<string, int>();

            (await CompanyFacade.GetAllMenuItems(User.Identity.Name)).ForEach(i => itemsSell.Add(i.Name, 0));
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
            orders.FindAll(o => o.OrderStartTime > DateTime.Now.AddMonths(-1)).ForEach(o => o.Items.ForEach(i => itemsSell[i.MenuItem.Name]++));

            return PartialView("MonthChart", itemsSell);
        }

    }
}