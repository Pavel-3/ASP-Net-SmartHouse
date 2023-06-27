using Microsoft.EntityFrameworkCore;
using SmartHouse.Abstractions.Data;
using SmartHouse.Abstractions.Data.Repositories;
using SmartHouse.Data;
using SmartHouse.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using System.Linq;
using SmartHouse.Core.DTOs;
using SmartHouse.Core;

namespace SmartHouse.Repositories.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {

        protected readonly HouseContext DbContext;
        protected readonly DbSet<TEntity> DbSet;
        public Repository(HouseContext houseContext)
        {
            DbContext = houseContext;
            DbSet = DbContext.Set<TEntity>();
        }

        //public virtual async Task<int> AddAsync(TEntity entity)
        //{
        //    var entityEntry = await DbSet.AddAsync(entity);
        //    return entityEntry.Entity.Id;
        //}
        public virtual int Add(TEntity entity)
        {
            var entityEntry = DbSet.Add(entity);
            return entityEntry.Entity.Id;
        }
        public virtual async Task AddRangeAsync(IEnumerable<TEntity?> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }
        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }


        public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var result = DbSet.Where(predicate);
            if (includes.Any())
            {
                result = includes
                    .Aggregate(result, (current, includes) => current.Include(includes));
            }
            return result;
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            var user = await DbSet.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id == id); 
            return user;
        }

        public virtual async Task RemoveAsync(int id)
        {
            var entry = await DbSet.FirstOrDefaultAsync(entity => entity.Id == id);
            DbSet.Remove(entry);
        }

        public virtual async Task RemoveRange(IEnumerable<TEntity?> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public virtual async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
        }
        public virtual IQueryable<TEntity> GetDbSetAsQueryable()
        {
            return DbSet;
        }
        public virtual async Task PatchAsync(int Id, IEnumerable<PatchDTO> patchDTOs)
        {
            var entity = await DbSet.FirstOrDefaultAsync(entity=> entity.Id == Id);

            var nameValuePairProperties = patchDTOs
                .ToDictionary(key => key.PropertyName,
                value => value.PropertyValue);
            var dbEntityEntry = DbSet.Entry(entity);
            dbEntityEntry.CurrentValues.SetValues(nameValuePairProperties);
            dbEntityEntry.State = EntityState.Modified;
        }
        public virtual async Task<int> CountAsync()
        {
            var count = await DbSet.CountAsync();
            return count;
        }
    }
}
