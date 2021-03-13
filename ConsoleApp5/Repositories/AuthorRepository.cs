using ConsoleApp.DAL;
using ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Repositories
{
    public class AuthorRepository : IDisposable
    {
        private readonly SampleDbContext dbContext;

        public AuthorRepository(SampleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        internal Task<List<Author>> GetAllAuthors()
        {
            return dbContext.Authors.ToListAsync();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
