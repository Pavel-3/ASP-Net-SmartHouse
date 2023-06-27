using AutoMapper;
using SmartHouse.Core.DTOs;
using SmartHouse.WebAPI.Requests;
using SmartHouse.WebAPI.Responses;

namespace SmartHouse.WebAPI.MappingProfiles
{
    public class AdminProfile :Profile
    {
        public AdminProfile() 
        {
            CreateMap<AdminDTO, AdminRequest>();
            CreateMap<AdminResponse, AdminDTO>();
        }
    }
}
