using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.DTO
{
    public static class AuthorsExtensions
    {
        public static List<Author> ToDTO(this List<Models.Author> authors) => authors.Select(x => new DTO.Author(x)).ToList();
    }
}
