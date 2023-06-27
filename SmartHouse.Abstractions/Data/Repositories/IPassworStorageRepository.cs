using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Data.Entities;
using SmartHouse.Data.PassworStorage;

namespace SmartHouse.Abstractions.Data.Repositories
{
    public interface IPassworStorageRepository
    {
        Task<Password> GetPasswordByUserId(int Id);
        Task<EntityEntry<Password>> AddPasswordAsync(Password password);
        public IQueryable<Password> FindBy(Expression<Func<Password, bool>> predicate);
        public Task RemoveAsync(int id);
        public void RemoveRange(IEnumerable<Password> entities);
    }
}
