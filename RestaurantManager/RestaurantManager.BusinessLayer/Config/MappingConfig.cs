using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantManager.BusinessLayer.DTOs;
using RestaurantManager.BusinessLayer.DTOs.DTOs;
using RestaurantManager.BusinessLayer.DTOs.Filters;
using RestaurantManager.BusinessLayer.QueryObjects;
using RestaurantManager.DAL.Models;
using RestaurantManager.Infrastructure.Query;

namespace RestaurantManager.BusinessLayer.Config
{
    public class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<Company, CompanyDto>().ReverseMap();
            config.CreateMap<Employee, EmployeeDto>().ReverseMap();
            config.CreateMap<MenuItem, MenuItemDto>().ReverseMap();
            config.CreateMap<Order, OrderDto>().ReverseMap();
            config.CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            config.CreateMap<Payment, PaymentDto>().ReverseMap();
            config.CreateMap<Stock, StockDto>().ReverseMap();
            config.CreateMap<Company, CompanyWithIncludesDto>().ReverseMap();

            config.CreateMap<QueryResult<Company>, QueryResultDto<CompanyDto, CompanyFilterDto>>();
        }
    }
}
