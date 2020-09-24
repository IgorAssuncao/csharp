using Biblioteca.Data;
using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.Repositories
{
    public class BookRepository
    {
        private BibliotecaContext BibliotecaContext { get; set; }

        public BookRepository (BibliotecaContext context)
        {
            BibliotecaContext = context;
        }

        public async Task<Book> GetById(Guid bookId)
        {
            Book book = await BibliotecaContext.BookSet.FindAsync(bookId);
            return book;
        }

        public async Task<List<Book>> GetAll()
        {
            return await BibliotecaContext.BookSet.ToListAsync();
        }

        public async Task<bool> Add(Book book)
        {
            try
            {
                await BibliotecaContext.BookSet.AddAsync(book);
                await BibliotecaContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Book book)
        {
            try
            {
                BibliotecaContext.BookSet.Update(book);
                await BibliotecaContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Remove(Book book)
        {
            try
            {
                BibliotecaContext.BookSet.Remove(book);
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
