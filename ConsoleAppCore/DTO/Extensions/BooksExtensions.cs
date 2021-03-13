using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.DTO
{
    public static class BooksExtensions
    {
        public static List<Book> ToDTO(this List<Models.Book> authors) => authors.Select(x => new Book(x)).ToList();
    }
}
