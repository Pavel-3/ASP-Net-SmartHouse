using AutoMapper;
using SmartHouse.Core.DTOs;
using SmartHouse.WebAPI.Responses;
using SmartHouse.WebAPI.Requests;
using SmartHouse.Data.Entities;

namespace SmartHouse.WebAPI.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<UserDTO, UserRequest>();
            CreateMap<UserWithDeviceValueDTO, UserRequest>();
            CreateMap<UserRequest, UserDTO>();
            CreateMap<UserDTO, UserResponse>();
            CreateMap<User,UserWithDeviceValueDTO>();
            CreateMap<UserWithDeviceValueDTO, UserResponse>();
            CreateMap<UserRequest, UserDTO>();
        }
    }
}
