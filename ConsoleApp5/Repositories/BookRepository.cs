using ConsoleApp.DAL;
using ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp.Repositories
{
    public class BookRepository : IDisposable
    {
        private readonly SampleDbContext dbContext;

        public BookRepository(SampleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        internal Task<List<Book>> GetBooksWithAuthors()
        {
            return dbContext.Books.Include(b => b.Author).ToListAsync();
        }

        internal Task Insert(Book book)
        {
            dbContext.Books.Add(book);
            return dbContext.SaveChangesAsync();
        }

        internal async Task UpdateBookTitle(int bookId, string bookTitle)
        {
            var bookForUpdate = await dbContext.Books.SingleAsync(x => x.Id == bookId);
            bookForUpdate.Title = bookTitle;
            await dbContext.SaveChangesAsync();
        }

        internal async Task Delete(int bookId)
        {
            var bookForDelete = await dbContext.Books.SingleAsync(x => x.Id == bookId);
            dbContext.Remove(bookForDelete);
            await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
