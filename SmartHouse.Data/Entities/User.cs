using SmartHouse.Core;

namespace SmartHouse.Data.Entities
{
    public class User : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public List<Room>? Rooms { get; set; }
        public List<Device>? Devices { get; set; }
    }
}
