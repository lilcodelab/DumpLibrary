using ConsoleApp.Models;
using ConsoleApp.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp.Service
{
    public class BookService: IDisposable
    {
        private readonly BookRepository repository;

        public BookService(BookRepository repository)
        {
            this.repository = repository;
        }

        internal Task<List<Book>> GetBooksWithAuthors()
        {
            return repository.GetBooksWithAuthors();
        }

        internal Task Insert(Book book)
        {
            return repository.Insert(book);
        }

        internal Task UpdateBookTitle(int bookId, string bookTitle)
        {
            return repository.UpdateBookTitle(bookId, bookTitle);
        }

        internal Task Delete(int bookId)
        {
            return repository.Delete(bookId);
        }

        public void Dispose()
        {
            repository?.Dispose();
        }
    }
}
