namespace SmartHouse.WebAPI.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<DeviceResponse> Devices { get; set; }
    }
}
