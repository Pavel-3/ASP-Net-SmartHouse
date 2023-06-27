namespace SmartHouse.MVC.Models
{
    public class DevicesWithPaginationModel
    {
        public List<DeviceWithValueModel> Devices { get; set; }
        public PageInfoModel PageInfo { get; set; }
    }
}
