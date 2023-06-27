using SmartHouse.Abstractions.Services;
using SmartHouse.Core.DTOs;
namespace SmartHouse.MVC.Models
{
    public class DeviceModel
    {
        public string Name { get; set; }
        public int RoomId { get; set; }
        public int DeviceId { get; set; }
        public DeviceType DeviceType { get; set; }
    }
}
