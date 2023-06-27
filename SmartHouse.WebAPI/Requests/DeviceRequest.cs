using SmartHouse.Core.DTOs;

namespace SmartHouse.WebAPI.Requests
{
    public class DeviceRequest
    {
        public string Name { get; set; }
        public string? UserDefinedName { get; set; }
        public DeviceType DeviceType { get; set; }
        public object? Value { get; set; }
    }
}
