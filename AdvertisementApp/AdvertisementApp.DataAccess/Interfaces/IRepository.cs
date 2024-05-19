using AdvertisementApp.Common.Enums;
using AdvertisementApp.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.DataAccess.Interfaces
{
    public interface IRepository<T> where T:BaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, TKey>> selector, OrderByType type = OrderByType.DESC);
        Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> selector, OrderByType type = OrderByType.DESC);
        Task<T> FindAsync(object id);
        Task<T> GetByFilter(Expression<Func<T, bool>> filter, bool asnoTracking = false);
        IQueryable<T> GetQuery();
        void Remove(T entity);
        Task Create(T entity);
        void Updated(T entity, T unchanged);

    }
}
