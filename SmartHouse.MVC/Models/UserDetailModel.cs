namespace SmartHouse.MVC.Models
{
    public class UserDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<DeviceWithValueModel> devices { get; set; }
    }
}
