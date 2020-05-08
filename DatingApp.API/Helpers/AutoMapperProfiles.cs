using Microsoft.AspNetCore.Http;
using AutoMapper;
using DatingApp.API.Models;
using DatingApp.API.DTO;
using System.Linq;
using System;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
                .ForMember( dest => dest.PhotoUrl, opt => 
                opt.MapFrom( src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember( dest => dest.Age, opt => opt.MapFrom(src =>  src.DateOfBirth.CalculateAge()));
            CreateMap<User, UserForDetailedDto>()
                .ForMember( dest => dest.PhotoUrl, opt => 
                opt.MapFrom( src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember( dest => dest.Age, opt => opt.MapFrom(src =>  src.DateOfBirth.CalculateAge()));
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Photo, PhotoForDetailedDto>();
            CreateMap<Photo,PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<UserForRegisterDto, User>().ForMember( dest => dest.Name, opt => opt.MapFrom( src => src.username));
            CreateMap<MessageForCreationDto, Message>().ReverseMap();
            CreateMap<Message, MessageToRetrunDto>()
                .ForMember( m => m.SenderPhotoUrl, opt => opt.MapFrom( u => u.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember( m => m.RecipientPhotoUrl, opt => opt.MapFrom( u => u.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url));
        }
    }
}
