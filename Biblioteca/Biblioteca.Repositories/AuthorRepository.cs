using Biblioteca.Data;
using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.Repositories
{
    public class AuthorRepository
    {
        private BibliotecaContext BibliotecaContext { get; set; }

        public AuthorRepository (BibliotecaContext context)
        {
            BibliotecaContext = context;
        }

        public async Task<Author> GetById(Guid authorId)
        {
            Author author = await BibliotecaContext.AuthorSet.FindAsync(authorId);
            return author;
        }

        public async Task<Author> GetByEmail(string email)
        {
            Author author = await BibliotecaContext.AuthorSet.FirstOrDefaultAsync(author => author.Email == email);
            return author;
        }

        public async Task<List<Author>> GetAll()
        {
            return await BibliotecaContext.AuthorSet.ToListAsync();
        }

        public async Task<bool> Add(Author author)
        {
            try
            {
                await BibliotecaContext.AuthorSet.AddAsync(author);
                await BibliotecaContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Author author)
        {
            try
            {
                BibliotecaContext.AuthorSet.Update(author);
                await BibliotecaContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Remove(Author author)
        {
            try
            {
                BibliotecaContext.AuthorSet.Remove(author);
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
