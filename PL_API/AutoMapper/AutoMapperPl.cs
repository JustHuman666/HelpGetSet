using PL_API.Models;
using AutoMapper;
using BLL.EntitiesDto;

namespace PL_API.AutoMapper
{
    public class AutoMapperPl: Profile
    {
        public AutoMapperPl()
        {
            CreateMap<UserDto, UserModel>().ReverseMap();

            CreateMap<UserDto, RegisterModel>().ReverseMap();

            CreateMap<MessageDto, MessageModel>().ReverseMap();

            CreateMap<CountryDto, CountryModel>().ReverseMap();

            CreateMap<MigrantDto, MigrantModel>()
                .ForMember(p => p.UserIds, c => c.MapFrom(src => src.UserIds))
                .ReverseMap();

            CreateMap<VolunteerDto, VolunteerModel>()
               .ForMember(p => p.UserIds, c => c.MapFrom(src => src.UserIds))
               .ReverseMap();

            CreateMap<PostDto, PostModel>().ReverseMap();

            CreateMap<CountryDto, CountryModel>().ReverseMap();

            CreateMap<UserProfileDto, UserProfileModel>()
                .ForMember(p => p.MessageIds, c => c.MapFrom(src => src.MessageIds))
                .ForMember(p => p.PostIds, c => c.MapFrom(src => src.PostIds))
                .ForMember(p => p.MadeCountryChangeIds, c => c.MapFrom(src => src.MadeCountryChangeIds))
                .ForMember(p => p.ChatIds, c => c.MapFrom(src => src.ChatIds))
                .ForMember(p => p.CountryIds, c => c.MapFrom(src => src.CountryIds))
                .ReverseMap();
        }
    }
}
