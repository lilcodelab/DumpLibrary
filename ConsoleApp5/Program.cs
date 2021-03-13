using ConsoleApp.DAL;
using ConsoleApp.Repositories;
using ConsoleApp.Service;
using DTO = ConsoleApp.DTO;
using Models = ConsoleApp.Models;
using Serilog;
using Serilog.Lilcodelab.Wrapper;
using System;
using System.Threading.Tasks;
using System.Linq;

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
                var authorRepository = new AuthorRepository(dbCtx);
                var authorService = new AuthorService(authorRepository);
                var bookRepository = new BookRepository(dbCtx);
                var bookService = new BookService(bookRepository);

                var authors = await authorService.GetAllAuthors();
                consoleEngine.Display(authors.Select(x=> new DTO.Author(x)).ToList());

                ConsoleActions optionCode = ConsoleActions.Exit;
                do
                {
                    optionCode = consoleEngine.ShowMenuAndGetOption();                    

                    switch (optionCode)
                    {
                        case ConsoleActions.List:
                            var books = await bookService.GetBooksWithAuthors();
                            consoleEngine.DisplayBooks(books.Select(x=> new DTO.Book(x)).ToList());                            
                            break;
                        case ConsoleActions.Insert:
                            var book = consoleEngine.GetBook();                            
                            await bookService.Insert(new Models.Book(book));
                            break;
                        case ConsoleActions.Update:
                            var bookForUpdate = consoleEngine.GetBookForUpdateTitle();                            
                            await bookService.UpdateBookTitle(bookForUpdate.Id, bookForUpdate.Title);
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
