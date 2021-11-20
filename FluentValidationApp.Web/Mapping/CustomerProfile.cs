using AutoMapper;
using FluentValidationApp.Web.Dtos;
using FluentValidationApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationApp.Web.Mapping
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.Adi, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Yasi, opt => opt.MapFrom(x => x.Age))
                .ForMember(dest => dest.Eposta, opt => opt.MapFrom(x => x.Email));
        }
    }
}
