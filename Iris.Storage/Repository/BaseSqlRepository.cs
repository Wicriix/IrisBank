using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Iris.Commons.Storage.Repository.Interface;

namespace Iris.Commons.Storage.Repository
{
    public abstract class BaseSqlRepository<T> : IBaseSqlRepository<T> where T : EntityBase
    {
        protected readonly DbContext _dbContext;
        private IQueryable<T> queriable;

        public BaseSqlRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), "Invalid DbContext provided.");
        }

        public async Task<IEnumerable<T>> SQLQuery(string query, ISpecification<T> spec)
        {
            return await ApplySpecification(query, spec);
        }

        public virtual void SetIQueryable(Func<IQueryable<T>, IQueryable<T>> func)
        {
            queriable = func.Invoke(_dbContext.Set<T>().AsQueryable());
        }

        public virtual async Task<T> GetByIdAsync<Z>(Z id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetByIdAsync(params object[] id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAll(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<IEnumerable<TResult>> ListSelectAsync<TResult>(ISpecification<T> spec, Expression<Func<T, TResult>> selector)
        {
            return await ApplySpecification(spec).Select(selector).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListNotTrackingAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).AsNoTracking().ToListAsync();
        }

        public async Task<long> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<long> CountNoTrackingAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).AsNoTracking().CountAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                _dbContext.Update(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<T> UpdateNotTrakingAsync(T entity)
        {
            try
            {
                _dbContext.Update(entity);

                return entity;
            }
            catch (DbUpdateException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task Save()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task AttachAsync(T entity, Func<T, T> func)
        {
            _dbContext.Attach(entity);
            _ = func.Invoke(entity);
            _dbContext.ChangeTracker.Entries()
                .Where(t => t.State == EntityState.Modified && t.Properties.Where(y => y.IsModified).Any())
                .ToList();
            await _dbContext.SaveChangesAsync();
        }

        public async Task AttachRangeAsync(IEnumerable<T> entity, Func<IEnumerable<T>, IEnumerable<T>> func)
        {
            _dbContext.AttachRange(entity);
            _ = func.Invoke(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> DeleteAsync(T entity)
        {
            try
            {
                _dbContext.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entity)
        {
            _dbContext.RemoveRange(entity);
            await _dbContext.SaveChangesAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(queriable ?? _dbContext.Set<T>().AsQueryable(), spec);
        }

        private async Task<IQueryable<T>> ApplySpecification(string query, ISpecification<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(query, _dbContext.Set<T>(), spec);
        }

        public Task<IDbContextTransaction> BeginTransaction()
        {
            return _dbContext.Database.BeginTransactionAsync();
        }

        public IQueryable<T> GetIQueryable()
        {
            return queriable ?? _dbContext.Set<T>().AsQueryable();
        }

        public IQueryable<T> GetIQueryable(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(queriable ?? _dbContext.Set<T>().AsQueryable(), spec);
        }

        public async Task<T> GetByIdAsync(params Enum[] @enum)
        {
            //var dato = @enum.Select(x=> Conversion.GetMensaje(x)).ToArray();
            var dato = new string[] { };
            return await _dbContext.Set<T>().FindAsync(dato);
        }

    }
}
