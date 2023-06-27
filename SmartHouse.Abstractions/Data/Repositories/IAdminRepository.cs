using Microsoft.EntityFrameworkCore.ChangeTracking;
using SmartHouse.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Abstractions.Data.Repositories
{
    public interface IAdminRepository : IRepository<Admin>
    {
        public Task<EntityEntry<Admin>> AddAsync(Admin admin);
    }
}
