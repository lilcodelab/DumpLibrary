using ConsoleApp.DTO;
using Serilog.Core;
using System;
using System.Collections.Generic;

namespace ConsoleApp.Service
{
    public class ConsoleEngine
    {
        private readonly Logger log;

        public ConsoleEngine(Logger log)
        {
            this.log = log;
        }

        public void Display(List<Author> authors)
        {
            foreach (var a in authors)
            {
                Console.WriteLine("[{0}] {1}", a.Id, a.FullName);
            }
        }

        public ConsoleActions ShowMenuAndGetOption()
        {
            try
            {
                Console.WriteLine("0)Exit\n1)List Books\n2)Insert new Book\n3)Update Book Title\n4)Delete Book\nOption:");
                var option = Console.ReadKey();
                Console.WriteLine();
                log.Information($"User pressed key: {option.Key}");
                var userConsoleAction = int.Parse(option.KeyChar.ToString());
                if (userConsoleAction > 4)
                    throw new ArgumentOutOfRangeException("Invalid Command");

                return (ConsoleActions)userConsoleAction;
            }
            catch (FormatException ex)
            {
                log.Warning(ex, ex.Message);
                Console.WriteLine("Invalid Input");
                return ShowMenuAndGetOption();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                log.Warning(ex, ex.Message);
                Console.WriteLine(ex.Message);
                return ShowMenuAndGetOption();
            }
        }

        public void DisplayBooks(List<Book> books)
        {
            foreach (var b in books)
            {
                Console.WriteLine("[{0}] {1}:{2}", b.Id, b.AuthorFullName, b.Title);
            }
        }

        public Book GetBook()
        {
            Console.WriteLine("Input Book Id");
            var bookId = int.Parse(Console.ReadLine());
            Console.WriteLine("Input Book Title");
            var bookTitle = Console.ReadLine();
            Console.WriteLine("Input Author Id");
            var authorId = int.Parse(Console.ReadLine());
            return new Book() { Id = bookId, AuthorId = authorId, Title = bookTitle };
        }

        public BookForUpdate GetBookForUpdateTitle()
        {
            Console.WriteLine("Input Book Id");
            var updateBookId = int.Parse(Console.ReadLine());
            Console.WriteLine("Input new Book Title");
            var updateBookTitle = Console.ReadLine();

            return new BookForUpdate() { Id = updateBookId, Title = updateBookTitle };
        }

        public int GetBookForDelete()
        {
            Console.WriteLine("Input Book Id");
            var deleteBookId = int.Parse(Console.ReadLine());
            return deleteBookId;
        }
    }
}
