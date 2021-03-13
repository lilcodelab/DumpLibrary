using ConsoleApp.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Repositories
{
    public class GenericRepository<T> : IDisposable where T : class
    {
        protected readonly SampleDbContext DbContext;
        protected readonly DbSet<T> DbSet;

        public GenericRepository(SampleDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        internal Task<List<T>> GetAll(string include = null)
        {
            IQueryable<T> querry = DbSet;
            if (!string.IsNullOrEmpty(include))
            {
                querry = querry.Include(include);
            }

            return querry.ToListAsync();
        }

        internal Task<T> GetById(int id)
        {
            return DbSet.FindAsync(id).AsTask();
        }

        internal void Insert(T model)
        {
            DbSet.Add(model);
        }

        internal void Update(T model)
        {
            DbSet.Attach(model);
            DbContext.Entry<T>(model).State = EntityState.Modified;
        }

        internal void Delete(T model)
        {
            DbContext.Remove(model);
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
