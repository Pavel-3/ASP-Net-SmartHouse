using AutoMapper.Configuration.Conventions;
using SmartHouse.Core.DTOs;

namespace SmartHouse.MVC.Models
{
    public class DeviceWithValueModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? UserDefinedName { get; set; }
        public DeviceType DeviceType { get; set; }
        public object? Value { get; set; }
    }
}
