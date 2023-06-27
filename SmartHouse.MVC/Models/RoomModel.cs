namespace SmartHouse.MVC.Models
{
    public class RoomModel
    {
        public string Name { get; set; }    

        public List<DeviceModel> deviceModels { get; set; }
    }
}
