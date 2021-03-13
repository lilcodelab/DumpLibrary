using ConsoleApp.Models;
using ConsoleApp.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp.Service
{
    public class AuthorService : IDisposable
    {
        private UnitOfWork unitOfWork;

        public AuthorService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            unitOfWork?.Dispose();
        }

        public Task<List<Author>> GetAllAuthors()
        {
            return unitOfWork.AuthorRepository.GetAll();
        }

    }
}
