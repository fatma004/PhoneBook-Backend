using AutoMapper;
using BL.Dto;
using DAL.Models;
using Type = DAL.Models.Type;

namespace BL.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            this.CreateMap<Type, TypeModel>().ReverseMap();
            this.CreateMap<Phone, PhoneModel>().ReverseMap();
            this.CreateMap<Contact, ContactModel>().ReverseMap();
        }
    }
}