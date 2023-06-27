using Microsoft.EntityFrameworkCore.ChangeTracking;
using SmartHouse.Abstractions.Data.Repositories;
using SmartHouse.Data;
using SmartHouse.Data.Entities;

namespace SmartHouse.Repositories.Implementation
{
    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        public AdminRepository(HouseContext houseContext) : base(houseContext) { }
        public async Task<EntityEntry<Admin>> AddAsync(Admin admin)
        {
            var entityEntry = await DbContext.Admins.AddAsync(admin);
            return entityEntry;
        }
    }
}
