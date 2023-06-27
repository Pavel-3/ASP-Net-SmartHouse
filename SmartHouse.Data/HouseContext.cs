using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
//using SmartHouse.Data.AdminDb.Entities;
using SmartHouse.Data.Entities;

namespace SmartHouse.Data
{
    public class HouseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<NuemericalSensor> NuemericalSensors { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<FeedbackDevice> FeedbackDevices { get; set; }
        public DbSet<NuemericalFeedbackDevice> NuemericalFeedbackDevices{ get; set; }
        public DbSet<Admin> Admins { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-D87V24D;Database=SmartHouseDatabase;Trusted_Connection=True;encrypt=false");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>().UseTpcMappingStrategy();

            modelBuilder.Entity<NuemericalFeedbackDevice>()
                .ToTable("NuemericalFeedbackDevice", tb => tb.Property(e => e.Id).UseIdentityColumn(1, 7));
            modelBuilder.Entity<Sensor>()
                .ToTable("Sensor", tb => tb.Property(e => e.Id).UseIdentityColumn(2, 7));
            modelBuilder.Entity<FeedbackDevice>()
                .ToTable("FeedbackDevice", tb => tb.Property(e => e.Id).UseIdentityColumn(3, 7));
            modelBuilder.Entity<NuemericalSensor>()
                .ToTable("NuemericalSensor", tb => tb.Property(e => e.Id).UseIdentityColumn(4, 7));
            modelBuilder.Entity<Admin>()
                .ToTable("Admin", tb => tb.Property(e => e.Id).UseIdentityColumn(1, 2));
            modelBuilder.Entity<User>()
                .ToTable("User", tb => tb.Property(e => e.Id).UseIdentityColumn(2, 2));
            modelBuilder.Entity<Sensor>()
                .Property(e => e.Value)
                .HasConversion<int>();
            modelBuilder
                .Entity<FeedbackDevice>()
                .Property(e => e.Value)
                .HasConversion<int>();
            modelBuilder.Entity<Device>()
                .HasOne(e => e.User)
                .WithMany(e => e.Devices)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Device>().HasOne(e => e.Room).WithMany(e => e.Devices).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
                .HasMany(user => user.Devices)
                .WithOne(e => e.User)
                .OnDelete(DeleteBehavior.SetNull);

        }
        public HouseContext(DbContextOptions<HouseContext> options) : base(options) { }
    }

}
 