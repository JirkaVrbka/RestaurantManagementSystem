﻿namespace RestaurantManager.BusinessLayer.DTOs.DTOs
{
    public class StockDto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int BuyPrice { get; set; }
        public int Amount { get; set; }
    }
}
