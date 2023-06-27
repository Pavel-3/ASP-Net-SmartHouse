using SmartHouse.Core;

namespace SmartHouse.Data.Entities
{
    public abstract class Device : IBaseEntity
    {
        public int Id { get; set; }
        public string? UserDefinedName { get; set; }
        public string Name { get; set; }
        public int? RoomId { get; set; }
        public Room? Room { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
