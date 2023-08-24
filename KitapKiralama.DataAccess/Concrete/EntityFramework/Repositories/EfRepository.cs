using KitapKiralama.DataAccess.Abstract.DataManagement;
using KitapKiralama.DataAccess.Concrete.EntityFramework.Context;
using KitapKiralama.Entity.Base;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KitapKiralama.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbset;
        public EfRepository(AppDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public async Task<EntityEntry<T>> AddAsync(T entity)
        {
            return await _dbset.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> Filter = null, params string[] IncludeParameters)
        {
            IQueryable<T> query = _dbset;

            if (Filter != null)
            {
                query = query.Where(Filter);
            }

            if (IncludeParameters.Length > 0)
            {
                foreach (var item in IncludeParameters)
                {
                    query = query.Include(item);
                }
            }

            return await Task.Run(() => query);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> Filter, params string[] IncludeParameters)
        {
            IQueryable<T> query = _dbset;
            query = query.Where(Filter);
            if (IncludeParameters.Length > 0)
            {
                foreach (var item in IncludeParameters)
                {
                    query = query.Include(item);
                }
            }

            return await query.SingleOrDefaultAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            await Task.Run(() => _dbset.Remove(entity));
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() => _dbset.Update(entity));
        }
    }
}
