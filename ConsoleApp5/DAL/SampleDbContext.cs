
using ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ConsoleApp.DAL
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //seed
            var uncleBob = new Author() { Id = 1, FullName = "Robert C. Martin" };
            var martinFowler = new Author() { Id = 2, FullName = "Martin Fowler" };
            var hejlsberg = new Author() { Id = 3, FullName = "Anders Hejlsberg" };
            var dump = new Author() { Id = 4, FullName = "Dump" };

            builder.Entity<Author>().HasData(uncleBob, martinFowler, hejlsberg, dump);

            var books = new List<Book>()
            {
                new Book()
                {
                    Id=1,
                    AuthorId=uncleBob.Id,
                    Title = "Agile Software Development, Principles, Patterns, and Practices"
                },
                new Book()
                {
                    Id=2,
                    AuthorId=uncleBob.Id,
                    Title = "Clean Code: A Handbook of Agile Software Craftsmanship"
                },
                new Book()
                {
                    Id=3,
                    AuthorId=uncleBob.Id,
                    Title = "The Clean Coder: A Code Of Conduct For Professional Programmers"
                },
                new Book()
                {
                    Id=4,
                    AuthorId=uncleBob.Id,
                    Title = "Clean Architecture: A Craftsman's Guide to Software Structure and Design"
                },
                new Book()
                {
                    Id=5,
                    AuthorId=uncleBob.Id,
                    Title = "Clean Agile: Back to Basics"
                },
                new Book()
                {
                    Id=6,
                    AuthorId=martinFowler.Id,
                    Title = "Refactoring: Improving the Design of Existing Code"
                },
                new Book()
                {
                    Id=7,
                    AuthorId=martinFowler.Id,
                    Title = "Planning Extreme Programming"
                },
                new Book()
                {
                    Id=8,
                    AuthorId=martinFowler.Id,
                    Title = "Patterns of Enterprise Application Architecture"
                },
                new Book()
                {
                    Id=9,
                    AuthorId=martinFowler.Id,
                    Title = "Refactoring: Improving the Design of Existing Code, Second Edition"
                },
                new Book()
                {
                    Id=10,
                    AuthorId=hejlsberg.Id,
                    Title = "The C# Programming Language"
                },
            };

            builder.Entity<Book>().HasData(books);
        }
    }
}
