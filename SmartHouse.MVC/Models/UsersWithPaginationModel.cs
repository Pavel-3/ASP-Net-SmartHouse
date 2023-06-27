namespace SmartHouse.MVC.Models
{
    public class UsersWithPaginationModel
    {
        public List<UserPreviewModel> Users { get; set; }
        public PageInfoModel PageInfo { get; set; }
    }
}
