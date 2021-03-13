using ConsoleApp.DAL;
using ConsoleApp.Repositories;
using ConsoleApp.Service;
using Serilog;
using Serilog.Lilcodelab.Wrapper;
using System;
using System.Threading.Tasks;
using System.Linq;
using ConsoleApp.DTO;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var log = new LoggerConfiguration()
               .WriteTo.File("log.txt")
               .AddMsSqlServerSink("Server=(localDB)\\MSSQLLocalDB;Initial Catalog=DumpLibrary;Integrated Security=True;")
               .CreateLogger();

            log.Information($"Application Started {Environment.UserName}");

            try
            {
                var consoleEngine = new ConsoleEngine(log);
                var dbCtx = new SampleDesignTimeDbContextFactory().CreateDbContext(args);
                var unitOfWork = new UnitOfWork(dbCtx);
                var authorService = new AuthorService(unitOfWork);
                var bookService = new BookService(unitOfWork);
                var controller = new DumpLibraryController(consoleEngine, authorService, bookService);

                await controller.DisplayAllAuthors();

                await controller.RunBookLoop();
            }
            catch (Exception ex)
            {
                log.Error(ex, "App error!");
            }

            log.Information("Application Ended");
        }
    }
}
