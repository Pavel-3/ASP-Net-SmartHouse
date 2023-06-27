using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Abstractions.Data.Repositories;
using SmartHouse.Data.PassworStorage;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using SmartHouse.Data;
using Microsoft.EntityFrameworkCore;
using SmartHouse.Data.Entities;

namespace SmartHouse.Repositories.Implementation
{
    public class PassworStorageRepository : IPassworStorageRepository
    {
        private readonly PassworStorageDbContext _adminDbContext;
        public PassworStorageRepository(PassworStorageDbContext adminDbContext) 
        {
            _adminDbContext = adminDbContext;
        }
        public async Task<EntityEntry<Password>> AddPasswordAsync(Password password)
        {
            var entityEntry = await _adminDbContext.Passwords.AddAsync(password);
            return entityEntry;
        }

        public IQueryable<Password> FindBy(Expression<Func<Password, bool>> predicate)
        {
            var result = _adminDbContext.Passwords.Where(predicate);
            return result;
        }

        public async Task<Password> GetPasswordByUserId(int id)
        {
            var password = await _adminDbContext.Passwords.FirstOrDefaultAsync(pass => pass.Id == id);
            return password;
        }

        public async Task RemoveAsync(int id)
        {
            var entry = await _adminDbContext.Passwords.FirstOrDefaultAsync(entity => entity.Id == id);
            _adminDbContext.Passwords.Remove(entry);
        }

        public void RemoveRange(IEnumerable<Password> entities)
        {
            _adminDbContext.Passwords.RemoveRange(entities);
        }
    }
}
