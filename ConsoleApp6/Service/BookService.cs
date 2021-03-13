using ConsoleApp.Models;
using ConsoleApp.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp.Service
{
    public class BookService: IDisposable
    {
        private readonly UnitOfWork unitOfWork;

        public BookService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        internal Task<List<Book>> GetBooksWithAuthors()
        {
            return unitOfWork.BookRepository.GetAll(nameof(Book.Author));
        }

        internal Task Insert(Book book)
        {
            unitOfWork.BookRepository.Insert(book);
            return unitOfWork.Save();
        }

        internal async Task Delete(int bookId)
        {
            var book = await unitOfWork.BookRepository.GetById(bookId);
            unitOfWork.BookRepository.Delete(book);
            await unitOfWork.Save();
        }

        public void Dispose()
        {
            unitOfWork?.Dispose();
        }

        internal async Task UpdateBookTitle(DTO.BookForUpdate bookForUpdate)
        {
            var book = await unitOfWork.BookRepository.GetById(bookForUpdate.Id);
            book.Title = bookForUpdate.Title;
            unitOfWork.BookRepository.Update(book);
            await unitOfWork.Save();
        }
    }
}
