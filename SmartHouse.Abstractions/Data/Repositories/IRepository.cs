using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SmartHouse.Core.DTOs;
using SmartHouse.Core;

namespace SmartHouse.Abstractions.Data.Repositories
{
    public interface IRepository<TEntity> 
        where TEntity : class, IBaseEntity
    {
        public Task<TEntity?> GetByIdAsync(int id);
        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        //public Task<int> AddAsync(TEntity entity);
        //public int Add(TEntity entity);
        public Task AddRangeAsync(IEnumerable<TEntity> entities);
        //public void AddRange(IEnumerable<TEntity> entities);
        public Task PatchAsync(int Id, IEnumerable<PatchDTO> patchDTOs);
        public Task Update(TEntity entity);
        public Task RemoveAsync(int id);
        public Task RemoveRange(IEnumerable<TEntity> entities);
        public Task<int> CountAsync();
        public IQueryable<TEntity> GetDbSetAsQueryable();
    }
}
