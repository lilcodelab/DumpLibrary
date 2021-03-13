using ConsoleApp.DAL;
using ConsoleApp.Service;
using Serilog;
using Serilog.Lilcodelab.Wrapper;
using System;
using System.Threading.Tasks;

namespace ConsoleApp1
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
                var dbCtx = new SampleDesignTimeDbContextFactory().CreateDbContext(args);
                var dbService = new DbService(dbCtx);
                var consoleEngine = new ConsoleEngine(log);

                var authors = await dbService.GetAllAuthors();
                consoleEngine.Display(authors);

                ConsoleActions optionCode = ConsoleActions.Exit;
                do
                {
                    optionCode = consoleEngine.ShowMenuAndGetOption();                    

                    switch (optionCode)
                    {
                        case ConsoleActions.List:
                            var books = await dbService.GetBooksWithAuthors();
                            consoleEngine.DisplayBooks(books);                            
                            break;
                        case ConsoleActions.Insert:
                            var book = consoleEngine.GetBook();                            
                            await dbService.Insert(book);
                            break;
                        case ConsoleActions.Update:
                            var bookForUpdate = consoleEngine.GetBookForUpdateTitle();                            
                            await dbService.UpdateBookTitle(bookForUpdate.Id, bookForUpdate.Title);
                            break;
                        case ConsoleActions.Delete:
                            var deleteBookId = consoleEngine.GetBookForDelete();
                            await dbService .Delete(deleteBookId);
                            break;
                    }
                }
                while (optionCode != ConsoleActions.Exit);
            }
            catch (Exception ex)
            {
                log.Error(ex, "App error!");
            }

            log.Information("Application Ended");
        }
    }
}
