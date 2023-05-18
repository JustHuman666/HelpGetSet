﻿using AutoMapper;
using BLL.EntitiesDto;
using DAL.Enteties;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AutoMapper
{
    /// <summary>
    /// Auto mapper profile for all dtos and enteties
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<User, UserDto>()
                .ForMember(p => p.FirstName, c => c.MapFrom(src => src.UserProfile.FirstName))
                .ForMember(p => p.LastName, c => c.MapFrom(src => src.UserProfile.LastName))
                .ForMember(p => p.Birthday, c => c.MapFrom(src => src.UserProfile.Birthday))
                .ForMember(p => p.Gender, c => c.MapFrom(src => src.UserProfile.Gender))
                .ReverseMap();

            CreateMap<UserProfile, UserProfileDto>()
                .ForMember(p => p.PhoneNumber, c => c.MapFrom(src => src.AppUser.PhoneNumber))
                .ForMember(p => p.UserName, c => c.MapFrom(src => src.AppUser.UserName))
                .ForMember(p => p.MessageIds, c => c.MapFrom(src => src.Messages.Select(item => item.Id)))
                .ForMember(p => p.ChatIds, c => c.MapFrom(src => src.Chats.Select(item => item.ChatId)))
                .ForMember(p => p.PostIds, c => c.MapFrom(src => src.Posts.Select(item => item.Id)))
                .ForMember(p => p.CountryIds, c => c.MapFrom(src => src.Countries.Select(item => item.CountryId)))
                .ForMember(p => p.MadeCountryChangeIds, c => c.MapFrom(src => src.MadeCountryChanges.Select(item => item.Id)))
                .ForMember(p => p.CountryVersionsChecked, c => c.MapFrom(src => src.CountryVersionsChecked.Select(item => item.Id)))
                .ForMember(p => p.MigrantsIds, c => c.MapFrom(src => src.Migrants.Select(item => item.Id)))
                .ForMember(p => p.VolunteersIds, c => c.MapFrom(src => src.Volunteers.Select(item => item.Id)));

            CreateMap<UserProfileDto, UserProfile>()
                .ForPath(p => p.AppUser.PhoneNumber, c => c.MapFrom(src => src.PhoneNumber))
                .ForPath(p => p.AppUser.UserName, c => c.MapFrom(src => src.UserName))
                .ForMember(p => p.Messages, c => c.MapFrom(src => src.MessageIds))
                .ForMember(p => p.Chats, c => c.MapFrom(src => src.ChatIds))
                .ForMember(p => p.Posts, c => c.MapFrom(src => src.PostIds))
                .ForMember(p => p.Countries, c => c.MapFrom(src => src.CountryIds))
                .ForMember(p => p.MadeCountryChanges, c => c.MapFrom(src => src.MadeCountryChangeIds))
                .ForMember(p => p.CountryVersionsChecked, c => c.MapFrom(src => src.CountryVersionsChecked))
                .ForMember(p => p.Migrants, c => c.MapFrom(src => src.MigrantsIds))
                .ForMember(p => p.Volunteers, c => c.MapFrom(src => src.VolunteersIds));

            CreateMap<UserProfileDto, UserDto>().ReverseMap();

            CreateMap<Message, MessageDto>().ReverseMap();

            CreateMap<Chat, ChatDto>()
                .ForMember(p => p.MessageIds, c => c.MapFrom(src => src.Messages.Select(item => item.Id)))
                .ForMember(p => p.UserIds, c => c.MapFrom(src => src.Users.Select(item => item.UserId)));

            CreateMap<ChatDto, Chat>()
                .ForMember(p => p.Messages, c => c.MapFrom(src => src.MessageIds))
                .ForMember(p => p.Users, c => c.MapFrom(src => src.UserIds));

            CreateMap<CountryChangesHistory, CountryChangesHistoryDto>()
              .ForMember(p => p.UsersWhoChecked, c => c.MapFrom(src => src.UsersWhoChecked.Select(item => item.Id)));


            CreateMap<CountryChangesHistoryDto, CountryChangesHistory>()
              .ForMember(p => p.UsersWhoChecked, c => c.MapFrom(src => src.UsersWhoChecked));

            CreateMap<Country, CountryDto>()
               .ForMember(p => p.UserIds, c => c.MapFrom(src => src.Users.Select(item => item.UserId)))
               .ForMember(p => p.PostIds, c => c.MapFrom(src => src.Posts.Select(item => item.Id)))
               .ForMember(p => p.CountryVersionIds, c => c.MapFrom(src => src.CountryVersions.Select(item => item.Id)));

            CreateMap<CountryDto, Country>()
               .ForMember(p => p.Users, c => c.MapFrom(src => src.UserIds))
               .ForMember(p => p.Posts, c => c.MapFrom(src => src.PostIds))
               .ForMember(p => p.CountryVersions, c => c.MapFrom(src => src.CountryVersionIds));

            CreateMap<int, UserChat>()
                .ForMember(dest => dest.UserId, m => m.MapFrom(src => src));

            CreateMap<int, Post>()
                .ForMember(dest => dest.Id, m => m.MapFrom(src => src));

            CreateMap<int, UserCountry>()
                .ForMember(dest => dest.CountryId, m => m.MapFrom(src => src));

            CreateMap<int, CountryChangesHistory>()
                .ForMember(dest => dest.Id, m => m.MapFrom(src => src));

            CreateMap<int, UserApprove>()
                .ForMember(dest => dest.Id, m => m.MapFrom(src => src));

            CreateMap<int, Message>()
                .ForMember(dest => dest.Id, m => m.MapFrom(src => src));

            CreateMap<int, UserProfile>()
               .ForMember(dest => dest.Id, m => m.MapFrom(src => src));
        }

    }
}
