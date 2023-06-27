namespace SmartHouse.MVC.Models;
public class UserPreviewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<string> Devices { get; set; }
}
