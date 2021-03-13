using ConsoleApp.DTO;
using ConsoleApp.Service;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class DumpLibraryController
    {
        private ConsoleEngine consoleEngine;
        private AuthorService authorService;
        private BookService bookService;

        public DumpLibraryController(ConsoleEngine consoleEngine, AuthorService authorService, BookService bookService)
        {
            this.consoleEngine = consoleEngine;
            this.authorService = authorService;
            this.bookService = bookService;
        }

        public async Task DisplayAllAuthors()
        {
            var authors = await authorService.GetAllAuthors();
            consoleEngine.Display(authors.ToDTO());
        }

        public async Task RunBookLoop()
        {
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
    }
}
