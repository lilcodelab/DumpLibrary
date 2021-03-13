using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ConsoleApp.DAL
{
    public class SampleDesignTimeDbContextFactory : IDesignTimeDbContextFactory<SampleDbContext>
    {
        public SampleDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<SampleDbContext>()
           .UseSqlServer("Server=(localDB)\\MSSQLLocalDB;Initial Catalog=DumpLibrary;Integrated Security=True;")
           .EnableSensitiveDataLogging()
           .Options;

            var dbContext = new SampleDbContext(options);

            return dbContext;
        }
    }
}
