using Biblioteca.Mappings;
using Biblioteca.Models;
using Biblioteca.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.Services
{
    public class AuthorBookService
    {
        private AuthorBookRepository AuthorBookRepository { get; set; }

        public AuthorBookService(AuthorBookRepository authorBookRepository)
        {
            AuthorBookRepository = authorBookRepository;
        }

        public async Task<bool> Add(Guid authorId, Guid bookId)
        {
            AuthorBook authorBook = new AuthorBook() { AuthorId = authorId, BookId = bookId };
            return await AuthorBookRepository.AddBookToAuthor(authorBook);
        }

        public async Task<List<AuthorBook>> GetAllBooksFromAuthor(Author author)
        {
            return await AuthorBookRepository.GetAllBooksFromAuthor(author);
        }
    }
}
