using Biblioteca.Data;
using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Repositories
{
    public class AuthorBookRepository
    {
        private BibliotecaContext BibliotecaContext { get; set; }

        public AuthorBookRepository(BibliotecaContext context)
        {
            BibliotecaContext = context;
        }

        public async Task<List<AuthorBook>> GetAllBooksFromAuthor(Author author)
        {
            return await BibliotecaContext.AuthorBookSet.Where(authorBook => authorBook.AuthorId == author.Id).ToListAsync();
        }

        public async Task<List<AuthorBook>> GetAllAuthorsFromBook(Book book)
        {
            return await BibliotecaContext.AuthorBookSet.Where(authorBook => authorBook.BookId == book.Id).ToListAsync();
        }

        public async Task<bool> AddBookToAuthor(AuthorBook authorBook)
        {
            try
            {
                await BibliotecaContext.AuthorBookSet.AddAsync(authorBook);
                await BibliotecaContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveBookFromAuthor(AuthorBook authorBook)
        {
            try
            {
                BibliotecaContext.AuthorBookSet.Remove(authorBook);
                await BibliotecaContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
