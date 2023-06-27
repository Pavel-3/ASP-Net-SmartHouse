using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SmartHouse.Core.DTOs;
using SmartHouse.Core;
using SmartHouse.Data.Entities;
using SmartHouse.Data;

namespace SmartHouse.MVC.MappingProfiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<Admin, AdminDTO>();
            CreateMap<AdminDTO, Admin>();
        }
    }
}
