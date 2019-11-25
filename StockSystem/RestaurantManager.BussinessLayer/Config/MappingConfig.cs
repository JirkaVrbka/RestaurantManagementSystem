using AutoMapper;
using RestaurantManager.BusinessLayer.DataTransferObjects;
using RestaurantManager.BusinessLayer.DataTransferObjects.Dtos;
using RestaurantManager.BusinessLayer.DataTransferObjects.Filters;
using RestaurantManager.DAL.Enums;
using RestaurantManager.DAL.Models;

namespace RestaurantManager.BusinessLayer.Config
{
    public class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<Company, CompanyIdDto>().ReverseMap();
            config.CreateMap<Company, CompanyUsersDto>().ReverseMap();
            config.CreateMap<Company, CompanyCreateDto>().ReverseMap();
            config.CreateMap<Company, CompanyWithPeopleDto>().ReverseMap();
            config.CreateMap<Company, CompanyUsersFilterDto>().ReverseMap();
            config.CreateMap<MenuItemAmount, MenuItemAmountDto>().ReverseMap();
            config.CreateMap<MenuItem, MenuItemDto>().ReverseMap();
            config.CreateMap<Payment, PaymentDto>().ReverseMap();
            config.CreateMap<Person, PersonCreateDto>().ReverseMap();
            config.CreateMap<Person, PersonDto>().ReverseMap();
            config.CreateMap<Person, PersonGetCompanyDto>().ReverseMap();
            config.CreateMap<StockItem, ItemSellingInfoDto>().ReverseMap();
        }
    }
}
