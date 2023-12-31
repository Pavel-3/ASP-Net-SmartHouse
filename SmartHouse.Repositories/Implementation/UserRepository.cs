﻿using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SmartHouse.Abstractions.Data.Repositories;
using SmartHouse.Data;
using SmartHouse.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Repositories.Implementation
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(HouseContext houseContext) : base(houseContext) { }
        public async Task<List<User>> GetUsersByPageAsync(int currentPage, int pageSize)
        {
            var result = await DbSet
                .Include(user => user.Devices)
                .OrderBy(user => user.Id)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return result;
        }
        public async Task<EntityEntry<User>> AddAsync(User user)
        {
            var entityEntry = await DbContext.Users.AddAsync(user);
            return entityEntry;
        }

        public async Task<User> GetUserWithDeviceByIdAsync(int id)
        {
            var user = await DbSet.AsNoTracking().Include(user => user.Devices).FirstOrDefaultAsync(user => user.Id == id);
            return user;
        }
    }
}
