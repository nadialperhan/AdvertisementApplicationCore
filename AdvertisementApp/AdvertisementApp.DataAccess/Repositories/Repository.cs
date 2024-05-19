using AdvertisementApp.Common.Enums;
using AdvertisementApp.DataAccess.Context;
using AdvertisementApp.DataAccess.Interfaces;
using AdvertisementApp.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.DataAccess.Repositories
{
    public class Repository<T>:IRepository<T> where T:BaseEntity
    {
        private readonly AdvertisementContext _advertisementContext;

        public Repository(AdvertisementContext advertisementContext)
        {
            _advertisementContext = advertisementContext;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _advertisementContext.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T,bool>> filter)
        {
            return await _advertisementContext.Set<T>().Where(filter).AsNoTracking().ToListAsync();
        }
        public async Task<List<T>> GetAllAsync<TKey>(Expression<Func<T,TKey>> selector,OrderByType type=OrderByType.DESC)
        {
            return  type==OrderByType.ASC? await _advertisementContext.Set<T>().AsNoTracking().OrderBy(selector).ToListAsync():await _advertisementContext.Set<T>().AsNoTracking().OrderByDescending(selector).ToListAsync();
        }
        public async Task<List<T>> GetAllAsync<TKey>(Expression<Func<T,bool>> filter,Expression<Func<T, TKey>> selector,OrderByType type=OrderByType.DESC)
        {
            return type == OrderByType.ASC ? await _advertisementContext.Set<T>().Where(filter).AsNoTracking().OrderBy(selector).ToListAsync() : await _advertisementContext.Set<T>().Where(filter).AsNoTracking().OrderByDescending(selector).ToListAsync();
        }
        public async Task<T> FindAsync(object id)
        {
            return await _advertisementContext.Set<T>().FindAsync(id);
        }
        public async Task<T> GetByFilter(Expression<Func<T,bool>> filter,bool asnoTracking=false)
        {
            return  !asnoTracking ? await _advertisementContext.Set<T>().SingleOrDefaultAsync(filter): await _advertisementContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter);
        }
        public IQueryable<T> GetQuery()
        {
            return _advertisementContext.Set<T>().AsQueryable();
        }
        public void Remove(T entity)
        {
            _advertisementContext.Set<T>().Remove(entity);
        }
        public async Task Create(T entity)
        {
           await _advertisementContext.Set<T>().AddAsync(entity);
        }
        public void Updated(T entity,T unchanged)
        {
            _advertisementContext.Entry(unchanged).CurrentValues.SetValues(entity);
        }
    }
}
