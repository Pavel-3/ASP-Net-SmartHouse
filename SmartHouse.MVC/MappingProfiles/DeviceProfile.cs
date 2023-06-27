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
            CreateMap<Device, DeviceDTO>().ConvertUsing(device => ConvertToDeviceDTO(device));
            CreateMap<DeviceDTO, Device>().ConvertUsing(deviceDTO => Convert(deviceDTO));
            CreateMap<DeviceModel, DeviceDTO>();
            CreateMap<Device, DeviceDTOWithValue>().ConvertUsing(device => ConvertToDeviceDTOWithValue(device));
            CreateMap<DeviceDTOWithValue, Device>().ConvertUsing(device => Convert(device));
            CreateMap<DeviceWithValueModel, DeviceDTOWithValue>();
            CreateMap<DeviceDTOWithValue, DeviceWithValueModel>();
        }
        private DeviceDTO ConvertToDeviceDTO(Device device)
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
        private Device Convert(DeviceDTOWithValue device)
        {
            switch (device.DeviceType)
            {
                case DeviceType.Sensor:
                    throw new ArgumentException("cannot be converted DeviceDTOWithValue with DeviceType.Sensor. Use Convertation from DeviceDTO");

                case DeviceType.NumericalSensor:
                    throw new ArgumentException("cannot be converted DeviceDTOWithValue with DeviceType.NumericalSensor. Use Convertation from DeviceDTO");
                case DeviceType.FeedbackDevice:
                    return new FeedbackDevice()
                    {
                        Id = device.Id,
                        Name = device.Name,
                        UserId = device.UserId,
                        RoomId = device.RoomId,
                        Value = (bool)device.Value
                    };
                case DeviceType.NumericalFeedbackDevice:
                    return new NuemericalFeedbackDevice()
                    {
                        Id = device.Id,
                        Name = device.Name,
                        UserId = device.UserId,
                        RoomId = device.RoomId,
                        Value = (float)device.Value
                    };
                default:
                    throw new ArgumentException();
            }
        }
        public DeviceDTOWithValue ConvertToDeviceDTOWithValue(Device device)
        {
            switch (device)
            {
                case Sensor sensor:
                    return new DeviceDTOWithValue()
                    {
                        Id = sensor.Id,
                        Name = sensor.Name,
                        UserId = sensor.UserId,
                        RoomId = sensor.RoomId,
                        DeviceType = DeviceType.Sensor,
                        Value = sensor.Value
                    };
                case NuemericalSensor nuemericalSensor:
                    return new DeviceDTOWithValue()
                    {
                        Id = device.Id,
                        Name = device.Name,
                        UserId = device.UserId,
                        RoomId = device.RoomId,
                        DeviceType = DeviceType.NumericalSensor,
                        Value = nuemericalSensor.Value
                    };
                case FeedbackDevice feedbackDevice:
                    return new DeviceDTOWithValue()
                    {
                        Id = device.Id,
                        Name = device.Name,
                        UserId = device.UserId,
                        RoomId = device.RoomId,
                        DeviceType = DeviceType.FeedbackDevice,
                        Value = feedbackDevice.Value
                    };
                case NuemericalFeedbackDevice nuemericalFeedbackDevice:
                    return new DeviceDTOWithValue()
                    {
                        Id = device.Id,
                        Name = device.Name,
                        UserId = device.UserId,
                        RoomId = device.RoomId,
                        DeviceType = DeviceType.NumericalFeedbackDevice,
                        Value = nuemericalFeedbackDevice.Value
                    };
                default:
                    throw new ArgumentException();
            }
        }
    }
}