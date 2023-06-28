using AutoMapper;
using SmartHouse.Core.DTOs;
using SmartHouse.Data.Entities;
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
            CreateMap<AdminRequest, AdminDTO>();
            CreateMap<AdminDTO, Admin>();
            CreateMap<Admin, AdminDTO>();
            CreateMap<AdminDTO, AdminResponse>();
        }
    }
}
