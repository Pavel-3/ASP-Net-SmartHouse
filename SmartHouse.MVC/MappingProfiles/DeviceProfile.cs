using AutoMapper;
using SmartHouse.Core.DTOs;
using SmartHouse.Data.Entities;
using SmartHouse.MVC.Models;

namespace SmartHouse.MVC.MappingProfiles
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<Device, DeviceDTO>().ConvertUsing(device => Convert(device));
            CreateMap<DeviceDTO, Device>().ConstructUsing(deviceDTO => Convert(deviceDTO));
            CreateMap<DeviceModel, DeviceDTO>();
        }
        private DeviceDTO Convert(Device device)
        {
            switch (device)
            {
                case Sensor Sensor:
                    return new DeviceDTO()
                    {
                        Id = device.Id,
                        Name = device.Name,
                        UserId = device.UserId,
                        RoomId = device.RoomId,
                        DeviceType = DeviceType.Sensor
                    };
                case NuemericalSensor nuemericalSensor:
                    return new DeviceDTO()
                    {
                        Id = device.Id,
                        Name = device.Name,
                        UserId = device.UserId,
                        RoomId = device.RoomId,
                        DeviceType = DeviceType.NumericalSensor
                    };
                case FeedbackDevice feedbackDevice:
                    return new DeviceDTO()
                    {
                        Id = device.Id,
                        Name = device.Name,
                        UserId = device.UserId,
                        RoomId = device.RoomId,
                        DeviceType = DeviceType.FeedbackDevice
                    };
                case NuemericalFeedbackDevice nuemericalFeedbackDevice:
                    return new DeviceDTO()
                    {
                        Id = device.Id,
                        Name = device.Name,
                        UserId = device.UserId,
                        RoomId = device.RoomId,
                        DeviceType = DeviceType.NumericalFeedbackDevice
                    };
                default:
                    throw new ArgumentException();
            }
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