using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var uncleBob = new Author() { Id = 1, FullName = "Robert C. Martin" };
            var martinFowler = new Author() { Id = 2, FullName = "Martin Fowler" };
            var hejlsberg = new Author() { Id = 3, FullName = "Anders Hejlsberg" };
            var dump = new Author() { Id = 4, FullName = "Dump" };
            var authors = new[] { uncleBob, martinFowler, hejlsberg, dump };

            foreach (var a in authors)
            {
                Console.WriteLine("[{0}] {1}", a.Id, a.FullName);
            }

            var books = new List<Book>()
            {
                new Book()
                {
                    Id=1,
                    Author=uncleBob,
                    Title = "Agile Software Development, Principles, Patterns, and Practices"
                },
                new Book()
                {
                    Id=2,
                    Author=uncleBob,
                    Title = "Clean Code: A Handbook of Agile Software Craftsmanship"
                },
                new Book()
                {
                    Id=3,
                    Author=uncleBob,
                    Title = "The Clean Coder: A Code Of Conduct For Professional Programmers"
                },
                new Book()
                {
                    Id=4,
                    Author=uncleBob,
                    Title = "Clean Architecture: A Craftsman's Guide to Software Structure and Design"
                },
                new Book()
                {
                    Id=5,
                    Author=uncleBob,
                    Title = "Clean Agile: Back to Basics"
                },
                new Book()
                {
                    Id=6,
                    Author=martinFowler,
                    Title = "Refactoring: Improving the Design of Existing Code"
                },
                new Book()
                {
                    Id=7,
                    Author=martinFowler,
                    Title = "Planning Extreme Programming"
                },
                new Book()
                {
                    Id=8,
                    Author=martinFowler,
                    Title = "Patterns of Enterprise Application Architecture"
                },
                new Book()
                {
                    Id=9,
                    Author=martinFowler,
                    Title = "Refactoring: Improving the Design of Existing Code, Second Edition"
                },
                new Book()
                {
                    Id=10,
                    Author=hejlsberg,
                    Title = "The C# Programming Language"
                },
            };

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
                        var book = new Book() { Id = bookId, Author = authors.Single(x => x.Id == authorId), Title = bookTitle };
                        books.Add(book);
                        break;
                    case 3:
                        Console.WriteLine("Input Book Id");
                        var updateBookId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Input new Book Title");
                        var updateBookTitle = Console.ReadLine();
                        var bookForUpdate = books.Single(x => x.Id == updateBookId);
                        bookForUpdate.Title = updateBookTitle;
                        break;
                    case 4:
                        Console.WriteLine("Input Book Id");
                        var deleteBookId = int.Parse(Console.ReadLine());
                        var bookForDelete = books.Single(x => x.Id == deleteBookId);
                        books.Remove(bookForDelete);
                        break;
                }
            }
            while (optionCode > 0);
        }
    }
}
