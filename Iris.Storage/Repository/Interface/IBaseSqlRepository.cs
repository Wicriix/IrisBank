using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Iris.Commons.Storage.Repository.Interface
{
    public interface IBaseSqlRepository<T> where T : EntityBase
    {
        Task<IEnumerable<T>> SQLQuery(string query, ISpecification<T> spec);
        void SetIQueryable(Func<IQueryable<T>, IQueryable<T>> func);
        IQueryable<T> GetIQueryable();
        IQueryable<T> GetIQueryable(ISpecification<T> spec);
        Task<T> GetByIdAsync<Z>(Z id);
        Task<T> GetByIdAsync(params Enum[] @enum);
        Task<T> GetByIdAsync(params object[] id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<IEnumerable<TResult>> ListSelectAsync<TResult>(ISpecification<T> spec, Expression<Func<T, TResult>> selector);
        Task<IReadOnlyList<T>> ListNotTrackingAsync(ISpecification<T> spec);
        Task AttachAsync(T entity, Func<T, T> func);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> UpdateNotTrakingAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entity);
        Task<long> CountAsync(ISpecification<T> spec);
        Task<long> CountNoTrackingAsync(ISpecification<T> spec);
        Task<IDbContextTransaction> BeginTransaction();
        Task Save();
    }


}
