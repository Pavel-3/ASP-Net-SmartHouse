using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Data.Entities;
using SmartHouse.Core.DTOs;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SmartHouse.Abstractions.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<IEnumerable<User>> GetUsersByPageAsync(int currentPage, int PageSize);
        public Task<EntityEntry<User>> AddAsync(User user);
    }
}
