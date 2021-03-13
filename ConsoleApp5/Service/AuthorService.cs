using ConsoleApp.Models;
using ConsoleApp.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp.Service
{
    public class AuthorService : IDisposable
    {
        private AuthorRepository authorRepository;

        public AuthorService(AuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        public void Dispose()
        {
            authorRepository?.Dispose();
        }

        internal Task<List<Author>> GetAllAuthors()
        {
            return authorRepository.GetAllAuthors();
        }

    }
}
