using AutoMapper;
using SmartHouse.Core.DTOs;
using SmartHouse.Data.Entities;
using SmartHouse.WebAPI.Requests;
using SmartHouse.WebAPI.Responses;

namespace SmartHouse.WebAPI.MappingProfiles
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile() 
        {
            CreateMap<DeviceDTOWithValue, DeviceResponse>();
            CreateMap<DeviceDTO, DeviceResponse>();
            CreateMap<DeviceRequest, DeviceDTO>();
            CreateMap<DeviceDTO, Device>().ConvertUsing(deviceDTO => Convert(deviceDTO));


        }
        private Device Convert(DeviceDTO device)
        {
            switch (device.DeviceType)
            {
                case DeviceType.Sensor:
                    return new Sensor()
                    {
                        Id = device.Id,
                        Name = device.Name,
                        UserId = device.UserId,
                        RoomId = device.RoomId
                    };
                case DeviceType.NumericalSensor:
                    return new NuemericalSensor()
                    {
                        Id = device.Id,
                        Name = device.Name,
                        UserId = device.UserId,
                        RoomId = device.RoomId,
                    };
                case DeviceType.FeedbackDevice:
                    return new FeedbackDevice()
                    {
                        Id = device.Id,
                        Name = device.Name,
                        UserId = device.UserId,
                        RoomId = device.RoomId,
                    };
                case DeviceType.NumericalFeedbackDevice:
                    return new NuemericalFeedbackDevice()
                    {
                        Id = device.Id,
                        Name = device.Name,
                        UserId = device.UserId,
                        RoomId = device.RoomId,
                    };
                default:
                    throw new ArgumentException();
            }

        }
    }
}
