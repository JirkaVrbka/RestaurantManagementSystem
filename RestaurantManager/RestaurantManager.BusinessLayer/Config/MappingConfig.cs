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
            config.CreateMap<StockItem, StockItemDto>().ReverseMap();
            config.CreateMap<Company, CompanyWithIncludesDto>().ReverseMap();
            config.CreateMap<Employee, EmployeeCreateDto>().ReverseMap();

            config.CreateMap<QueryResult<Company>, QueryResultDto<CompanyDto, CompanyFilterDto>>();
            config.CreateMap<QueryResult<Employee>, QueryResultDto<EmployeeDto, EmployeeFilterDto>>();
            config.CreateMap<QueryResult<MenuItem>, QueryResultDto<MenuItemDto, MenuItemFilterDto>>();
            config.CreateMap<QueryResult<Order>, QueryResultDto<OrderDto, OrderFilterDto>>();
            config.CreateMap<QueryResult<OrderItem>, QueryResultDto<OrderItemDto, OrderFilterDto>>();
            config.CreateMap<QueryResult<Payment>, QueryResultDto<PaymentDto, PaymentFilterDto>>();
            config.CreateMap<QueryResult<StockItem>, QueryResultDto<StockItemDto, StockItemFilterDto>>();
        }
    }
}
