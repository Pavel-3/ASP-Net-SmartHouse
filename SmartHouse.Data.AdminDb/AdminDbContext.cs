using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace SmartHouse.Data.PassworStorage
{
    public class PassworStorageDbContext : DbContext
    {
        public DbSet<Password> Passwords { get; set; }
        public PassworStorageDbContext(DbContextOptions<PassworStorageDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        public PassworStorageDbContext() { }
    }
}