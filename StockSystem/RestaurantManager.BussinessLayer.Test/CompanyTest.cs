using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantManager.BusinessLayer.DataTransferObjects;
using RestaurantManager.DAL.Models;

namespace RestaurantManager.BusinessLayer.Test
{
    [TestClass]
    public class CompanyTest
    {
        [TestMethod]
        public void getCompany()
        {
            CompanyService<Company, CompanyDto, FilterBase>
        }
    }
}
