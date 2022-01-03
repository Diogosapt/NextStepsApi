using AutoMapper;
using System.Collections.Generic;

namespace NextSteps.Adpater.SQL.Configuration
{
    public class ContextAutoMapperProfile : Profile
    {
        public ContextAutoMapperProfile()
        {
            CreateMap<Business.Models.Person, Models.Person>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(x => x.Job, opt => opt.MapFrom(src => src.Job))
                .ForMember(x => x.Birthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.Hobbies, opt => opt.MapFrom(src => src.Hobbies))

                .ReverseMap();

            CreateMap<Business.Models.Hobbies, Models.Hobbies>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Hobby, opt => opt.MapFrom(src => src.Hobby))

                .ReverseMap();

            CreateMap<Business.Models.Filters, Models.Filters>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(x => x.Job, opt => opt.MapFrom(src => src.Job))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))

                .ReverseMap();
        }
    }
}