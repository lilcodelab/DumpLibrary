using ConsoleApp.DAL;
using ConsoleApp.Models;
using Dump.Repository;
using System;
using System.Threading.Tasks;

namespace ConsoleApp.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private readonly SampleDbContext DbContext;

        public UnitOfWork(SampleDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public GenericRepository<Author> AuthorRepository => new GenericRepository<Author>(DbContext);
        public GenericRepository<Book> BookRepository => new GenericRepository<Book>(DbContext);


        public void Dispose()
        {
            DbContext.Dispose();
        }

        public Task<int> Save()
            => DbContext.SaveChangesAsync();
    }
}
