using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Core.DTOs
{
    public class DeviceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? UserId { get; set; }
        public int? RoomId { get; set; }
        public DeviceType? DeviceType { get; set; }
    }
    public enum DeviceType
    {
        Sensor = 0,
        NumericalSensor = 1,
        FeedbackDevice = 2,
        NumericalFeedbackDevice = 3
    }
}
