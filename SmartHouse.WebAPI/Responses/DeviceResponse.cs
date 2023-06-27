using SmartHouse.Core.DTOs;

namespace SmartHouse.WebAPI.Responses
{
    public class DeviceResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? UserDefinedName { get; set; }
        public int UserId { get; set; }
        public DeviceType DeviceType { get; set; }
        public object? Value { get; set; }
    }
}