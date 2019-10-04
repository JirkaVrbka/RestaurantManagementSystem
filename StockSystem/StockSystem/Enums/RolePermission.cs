using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Enums
{
    public enum RolePermission
    {
        CanCreateOrder,
        CanCloseOrder,
        CanEndDay,
        CanBuyIntoStock,
        CanSetRole,
        CanCreateRole,
        CanSeeFinancialGraphs,
        CanSeeStock,
        CanSeeStockGraphs,
        CanCreateItem
    }
}
