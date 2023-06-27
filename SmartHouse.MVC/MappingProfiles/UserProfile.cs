using AutoMapper;
using AutoMapper.Configuration;
using SmartHouse.Core.DTOs;
using SmartHouse.Data.Entities;
using SmartHouse.MVC.Models;

namespace SmartHouse.MVC.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<UserDTO, UserPreviewModel>().ForMember(userPreview => userPreview.Devices, opt => opt.MapFrom(userDTO => userDTO.Devices.Select(user => user.Name)));
            CreateMap<CreateUserModel, UserDTO>();
        }
    }
}
