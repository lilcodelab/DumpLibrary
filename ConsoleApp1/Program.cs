using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var dbContext = new SampleDesignTimeDbContextFactory().CreateDbContext(args);

            var authors = await dbContext.Authors.ToListAsync();
            foreach (var a in authors)
            {
                Console.WriteLine("[{0}] {1}", a.Id, a.FullName);
            }


            int optionCode = 0;
            do
            {
                Console.WriteLine("0)Exit\n1)List Books\n2)Insert new Book\n3)Update Book Title\n4)Delete Book\nOption:");
                var option = Console.ReadKey();
                optionCode = int.Parse(option.KeyChar.ToString());
                Console.WriteLine();

                switch (optionCode)
                {
                    case 1:
                        var books = await dbContext.Books.Include(b=>b.Author).ToArrayAsync();
                        foreach (var b in books)
                        {
                            Console.WriteLine("[{0}] {1}:{2}", b.Id, b.Author.FullName, b.Title);
                        }
                        break;
                    case 2:
                        Console.WriteLine("Input Book Id");
                        var bookId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Input Book Title");
                        var bookTitle = Console.ReadLine();
                        Console.WriteLine("Input Author Id");
                        var authorId = int.Parse(Console.ReadLine());
                        var book = new Book() { Id = bookId, AuthorId = authorId, Title = bookTitle };
                        dbContext.Books.Add(book);
                        await dbContext.SaveChangesAsync();
                        break;
                    case 3:
                        Console.WriteLine("Input Book Id");
                        var updateBookId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Input new Book Title");
                        var updateBookTitle = Console.ReadLine();
                        var bookForUpdate = await dbContext.Books.SingleAsync(x => x.Id == updateBookId);
                        bookForUpdate.Title = updateBookTitle;
                        await dbContext.SaveChangesAsync();
                        break;
                    case 4:
                        Console.WriteLine("Input Book Id");
                        var deleteBookId = int.Parse(Console.ReadLine());
                        var bookForDelete = await dbContext.Books.SingleAsync(x => x.Id == deleteBookId);
                        dbContext.Remove(bookForDelete);
                        await dbContext.SaveChangesAsync();
                        break;
                }
            }
            while (optionCode > 0);

        }
    }
}
