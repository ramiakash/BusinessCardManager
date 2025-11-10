using AutoMapper;
using BusinessCardManager.Application.DTOs;
using BusinessCardManager.Domain.Entities;

namespace BusinessCardManager.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BusinessCard, BusinessCardDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone.Value))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.Name));
        }
    }
}
