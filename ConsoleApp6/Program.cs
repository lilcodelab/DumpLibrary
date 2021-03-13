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

                var authors = await authorService.GetAllAuthors();
                consoleEngine.Display(authors.ToDTO());

                ConsoleActions optionCode = ConsoleActions.Exit;
                do
                {
                    optionCode = consoleEngine.ShowMenuAndGetOption();                    

                    switch (optionCode)
                    {
                        case ConsoleActions.List:
                            var books = await bookService.GetBooksWithAuthors();
                            consoleEngine.DisplayBooks(books.ToDTO());                            
                            break;
                        case ConsoleActions.Insert:
                            var book = consoleEngine.GetBook();
                            await bookService.Insert(book.ToModel());
                            break;
                        case ConsoleActions.Update:
                            var bookForUpdate = consoleEngine.GetBookForUpdateTitle();                            
                            await bookService.UpdateBookTitle(bookForUpdate);
                            break;
                        case ConsoleActions.Delete:
                            var deleteBookId = consoleEngine.GetBookForDelete();
                            await bookService.Delete(deleteBookId);
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
