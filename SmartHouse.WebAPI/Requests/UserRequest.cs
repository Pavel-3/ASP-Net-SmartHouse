using SmartHouse.WebAPI.Responses;

namespace SmartHouse.WebAPI.Requests
{
    public class UserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<DeviceRequest> Devices { get; set; }
    }
}
