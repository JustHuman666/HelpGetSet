using PL_API.Models;
using AutoMapper;
using BLL.EntitiesDto;
using EnumTypes;
using DAL.Entities;

namespace PL_API.AutoMapper
{
    public class AutoMapperPl: Profile
    {
        public AutoMapperPl()
        {
            CreateMap<UserDto, UserModel>().ForMember(p => p.Gender, c => c.MapFrom(src => src.Gender.ToString()));

            CreateMap<UserDto, RegisterModel>().ForMember(p => p.Gender, c => c.MapFrom(src => src.Gender.ToString()));

            CreateMap<UserModel, UserDto>().ForMember(p => p.Gender, c => c.MapFrom(src => Enum.Parse(typeof(Gender), src.Gender)));

            CreateMap<RegisterModel, UserDto>().ForMember(p => p.Gender, c => c.MapFrom(src => Enum.Parse(typeof(Gender), src.Gender)));

            CreateMap<MessageDto, MessageModel>().ReverseMap();

            CreateMap<CountryDto, CountryModel>().ReverseMap();

            CreateMap<MigrantDto, MigrantModel>()
                .ForMember(p => p.FamilyStatus, c => c.MapFrom(src => src.FamilyStatus.ToString()))
                .ForMember(p => p.Housing, c => c.MapFrom(src => src.Housing.ToString()));

            CreateMap<MigrantModel, MigrantDto>()
                .ForMember(p => p.FamilyStatus, c => c.MapFrom(src => Enum.Parse(typeof(FamilyStatus), src.FamilyStatus)))
                .ForMember(p => p.Housing, c => c.MapFrom(src => Enum.Parse(typeof(Housing), src.Housing)));

            CreateMap<PostDto, PostModel>().ReverseMap();

            CreateMap<UserProfileDto, UserProfileModel>()
                .ForMember(p => p.Gender, c => c.MapFrom(src => src.Gender.ToString()))
                .ForMember(p => p.MessageIds, c => c.MapFrom(src => src.MessageIds))
                .ForMember(p => p.PostIds, c => c.MapFrom(src => src.PostIds))
                .ForMember(p => p.MadeCountryChangeIds, c => c.MapFrom(src => src.MadeCountryChangeIds))
                .ForMember(p => p.ChatIds, c => c.MapFrom(src => src.ChatIds))
                .ForMember(p => p.CountryIds, c => c.MapFrom(src => src.CountryIds))
                .ForMember(p => p.CountryVersionsChecked, c => c.MapFrom(src => src.CountryVersionsChecked))
                .ForMember(p => p.MigrantsIds, c => c.MapFrom(src => src.MigrantsIds))
                .ForMember(p => p.VolunteersIds, c => c.MapFrom(src => src.VolunteersIds));

            CreateMap<UserProfileModel, UserProfileDto>()
                .ForMember(p => p.Gender, c => c.MapFrom(src => Enum.Parse(typeof(Gender), src.Gender)))
                .ForMember(p => p.MessageIds, c => c.MapFrom(src => src.MessageIds))
                .ForMember(p => p.PostIds, c => c.MapFrom(src => src.PostIds))
                .ForMember(p => p.MadeCountryChangeIds, c => c.MapFrom(src => src.MadeCountryChangeIds))
                .ForMember(p => p.ChatIds, c => c.MapFrom(src => src.ChatIds))
                .ForMember(p => p.CountryIds, c => c.MapFrom(src => src.CountryIds))
                .ForMember(p => p.CountryVersionsChecked, c => c.MapFrom(src => src.CountryVersionsChecked))
                .ForMember(p => p.MigrantsIds, c => c.MapFrom(src => src.MigrantsIds))
                .ForMember(p => p.VolunteersIds, c => c.MapFrom(src => src.VolunteersIds))
                .ReverseMap();

            CreateMap<CountryModel, CountryChangesHistoryModel>()
                .ForMember(p => p.CountryId, c => c.MapFrom(src => src.Id))
                .ForMember(p => p.ShortName, c => c.MapFrom(src => src.ShortName))
                .ForMember(p => p.Name, c => c.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<CountryChangesHistoryDto, CountryChangesHistoryModel>().ReverseMap();
        }
    }
}
