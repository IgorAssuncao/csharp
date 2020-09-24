using Biblioteca.Mappings;
using Biblioteca.Models;
using Biblioteca.Repositories;
using Biblioteca.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.Services
{
    public class AuthorService
    {
        private AuthorRepository AuthorRepository { get; set; }
        private AuthorBookRepository AuthorBookRepository { get; set; }
        private BookRepository BookRepository { get; set; }

        public AuthorService(AuthorRepository authorRepository, AuthorBookRepository authorBookRepository, BookRepository bookRepository)
        {
            AuthorRepository = authorRepository;
            AuthorBookRepository = authorBookRepository;
            BookRepository = bookRepository;
        }

        public async Task<AuthorMapping> GetById(Guid id)
        {
            Author author = await AuthorRepository.GetById(id);
            if (author == null) return null;
            List<AuthorBook> books = await AuthorBookRepository.GetAllBooksFromAuthor(author);
            author.Books = books;
            AuthorMapping authorMapping = Helpers.ConvertAuthorToAuthorResponse(author);
            return authorMapping;
        }

        public async Task<List<AuthorMapping>> GetAll()
        {
            List<Author> authors = await AuthorRepository.GetAll();
            List<AuthorMapping> authorsMapped = new List<AuthorMapping>();

            foreach (Author author in authors)
            {
                List<AuthorBook> books = await AuthorBookRepository.GetAllBooksFromAuthor(author);
                author.Books = books;
                authorsMapped.Add(Helpers.ConvertAuthorToAuthorResponse(author));
            }

            return authorsMapped;
        }

        public async Task<bool> Add(string name, string lastname, string email, string password, DateTime birthday)
        {
            Guid id = Guid.NewGuid();
            Author author = new Author()
            {
                Id = id,
                Name = name,
                Lastname = lastname,
                Email = email,
                Password = password,
                Birthday = birthday,
                Books = new List<AuthorBook>()
            };

            return await AuthorRepository.Add(author);
        }

        public async Task<bool> Update(Guid id, string name, string lastname, string email, string password, DateTime birthday)
        {
            Author author = await AuthorRepository.GetById(id);

            if (author == null)
                return false;

            if (!string.IsNullOrEmpty(name))
                author.Name = name;
            if (!string.IsNullOrEmpty(lastname))
                author.Lastname = lastname;
            if (!string.IsNullOrEmpty(email))
                author.Email = email;
            if (!string.IsNullOrEmpty(password))
                author.Password = password;
            if (birthday.CompareTo(new DateTime(0001, 01, 01)) == 0)
                author.Birthday = birthday;

            return await AuthorRepository.Update(author);
        }

        public async Task<bool> Remove(Guid id)
        {
            Author author = await AuthorRepository.GetById(id);

            if (author == null)
                return false;

            return await AuthorRepository.Remove(author);
        }
    }
}
