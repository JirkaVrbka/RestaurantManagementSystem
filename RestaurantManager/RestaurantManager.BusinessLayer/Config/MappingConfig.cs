﻿using System;
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
            config.CreateMap<Company, CompanyUpdateNameDto>().ReverseMap();
            config.CreateMap<Company, CompanyWithEmployeesDto>().ReverseMap();
            config.CreateMap<Company, CompanyWithMenuItemsDto>().ReverseMap();
            config.CreateMap<Company, CompanyWithOrdersDto>().ReverseMap();
            config.CreateMap<Company, CompanyWithPaymentsDto>().ReverseMap();
            config.CreateMap<Company, CompanyWithStockItemsDto>().ReverseMap();
            config.CreateMap<Employee, EmployeeDto>().ReverseMap();
            config.CreateMap<MenuItem, MenuItemDto>().ReverseMap();
            config.CreateMap<Order, OrderDto>().ReverseMap();
            config.CreateMap<Order, OrderWithFullDependencyDto>().ReverseMap();
            config.CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            config.CreateMap<OrderItem, OrderItemWithMenuItemDto>().ReverseMap();
            config.CreateMap<Payment, PaymentDto>().ReverseMap();
            config.CreateMap<Company, CompanyWithIncludesDto>().ReverseMap();
            config.CreateMap<Employee, EmployeeCreateDto>().ReverseMap();


            config.CreateMap<QueryResult<Company>, QueryResultDto<CompanyDto, CompanyFilterDto>>();
            config.CreateMap<QueryResult<Employee>, QueryResultDto<EmployeeDto, EmployeeFilterDto>>();
            config.CreateMap<QueryResult<MenuItem>, QueryResultDto<MenuItemDto, MenuItemFilterDto>>();
            config.CreateMap<QueryResult<Order>, QueryResultDto<OrderDto, OrderFilterDto>>();
            config.CreateMap<QueryResult<Order>, QueryResultDto<OrderWithFullDependencyDto, OrderFilterDto>>();
            config.CreateMap<QueryResult<Order>, QueryResultDto<OrderDto, OrderClosedFilterDto>>();
            config.CreateMap<QueryResult<OrderItem>, QueryResultDto<OrderItemDto, OrderItemFilterDto>>();
            config.CreateMap<QueryResult<OrderItem>, QueryResultDto<OrderItemDto, OrderItemFilterByOrderDto>>();
            config.CreateMap<QueryResult<OrderItem>, QueryResultDto<OrderItemWithMenuItemDto, OrderItemFilterDto>>();
            config.CreateMap<QueryResult<OrderItem>, QueryResultDto<OrderItemWithMenuItemDto, OrderItemFilterByOrderDto>>();
            config.CreateMap<QueryResult<Payment>, QueryResultDto<PaymentDto, PaymentFilterDto>>();
        }
    }
}
