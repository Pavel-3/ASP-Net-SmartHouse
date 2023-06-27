using AutoMapper;
using SmartHouse.Core.DTOs;
using SmartHouse.WebAPI.Responses;

namespace SmartHouse.WebAPI.MappingProfiles
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile() 
        {
            CreateMap<DeviceDTOWithValue, DeviceResponse>();
            CreateMap<DeviceDTO, DeviceResponse>();
        }
    }
}
