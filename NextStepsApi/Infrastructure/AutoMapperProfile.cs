using AutoMapper;
using NextSteps.Api.Dto;
using NextSteps.Business.Models;
using System;

namespace NextSteps.Api.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PersonCreateDto, Person>()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(x => x.Birthday, opt => opt.MapFrom(src => ((DateTime)src.Birthday).ToShortDateString()))
                .ReverseMap();

            CreateMap<PersonDto, Person>().ReverseMap();

            CreateMap<FilterPersonDto, Filters>().ReverseMap();

            CreateMap<HobbiesCreateDto, Hobbies>()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ReverseMap();

            CreateMap<HobbiesDto, Hobbies>().ReverseMap();
        }
    }
}