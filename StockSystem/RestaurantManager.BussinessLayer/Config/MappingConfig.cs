using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManager.BussinessLayer.DataTransferObjects;
using RestaurantManager.DAL.Models;

namespace RestaurantManager.BussinessLayer.Config
{
    public class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<Company, CompanyIdDto>().ReverseMap();
            config.CreateMap<Company, CompanyUsersDto>().ReverseMap();
            config.CreateMap<Company, CompanyUsersFilterDto>().ReverseMap();
            config.CreateMap<PaymentInfo, PaymentInfoDto>().ReverseMap();
            config.CreateMap<Payment, PaymentDto>().ReverseMap();
            config.CreateMap<Role, RoleCreateDto>().ReverseMap();
            config.CreateMap<Person, PersonDto>().ReverseMap();
            config.CreateMap<Person, PersonGetCompanyDto>().ReverseMap();
            config.CreateMap<Item, ItemSellingInfoDto>().ReverseMap();
        }
    }
}
