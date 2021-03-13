using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dump.Repository
{
    public class GenericRepository<T> : IDisposable where T : class
    {
        protected readonly DbContext DbContext;
        protected readonly DbSet<T> DbSet;

        public GenericRepository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        public Task<List<T>> GetAll(string include = null)
        {
            IQueryable<T> querry = DbSet;
            if (!string.IsNullOrEmpty(include))
            {
                querry = querry.Include(include);
            }

            return querry.ToListAsync();
        }

        public Task<T> GetById(int id)
        {
            return DbSet.FindAsync(id).AsTask();
        }

        public void Insert(T model)
        {
            DbSet.Add(model);
        }

        public void Update(T model)
        {
            DbSet.Attach(model);
            DbContext.Entry<T>(model).State = EntityState.Modified;
        }

        public void Delete(T model)
        {
            DbContext.Remove(model);
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
