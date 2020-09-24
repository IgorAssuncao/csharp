using Biblioteca.Mappings;
using Biblioteca.Models;
using Biblioteca.Repositories;
using Biblioteca.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Services
{
    public class BookService
    {
        private BookRepository BookRepository { get; set; }
        private AuthorRepository AuthorRepository { get; set; }
        private AuthorBookRepository AuthorBookRepository  { get; set; }

        public BookService(BookRepository bookRepository, AuthorRepository authorRepository, AuthorBookRepository authorBookRepository)
        {
            BookRepository = bookRepository;
            AuthorRepository = authorRepository;
            AuthorBookRepository = authorBookRepository;
        }

        public async Task<BookMapping> GetById(Guid bookId)
        {
            Book book = await BookRepository.GetById(bookId);
            if (book == null) return null;

            book.Authors = await AuthorBookRepository.GetAllAuthorsFromBook(book);
            BookMapping bookMapped = Helpers.ConvertBookToBookResponse(book);
            return bookMapped;
        }

        public async Task<List<BookMapping>> GetAll()
        {
            List<Book> books = await BookRepository.GetAll();
            List<BookMapping> booksMapped = new List<BookMapping>();

            foreach (Book book in books)
            {
                book.Authors = await AuthorBookRepository.GetAllAuthorsFromBook(book);
                booksMapped.Add(Helpers.ConvertBookToBookResponse(book));
            }

            return booksMapped;
        }

        public async Task<bool> Add(string title, string isbn, string year, List<Guid> authorsIds)
        {
            Guid id = Guid.NewGuid();

            Book book = new Book
            {
                Id = id,
                Title = title,
                ISBN = isbn,
                Year = year,
                Authors = new List<AuthorBook>()
            };

            List<Guid> validAuthors = new List<Guid>();

            foreach(Guid guid in authorsIds)
            {
                Author author = await AuthorRepository.GetById(guid);
                if (author != null)
                {
                    validAuthors.Add(guid);
                    AuthorBook relationship = new AuthorBook { AuthorId = guid, BookId = id };
                    book.Authors.Add(relationship);
                    if (author.Books == null)
                        author.Books = new List<AuthorBook>();
                    author.Books.Add(relationship);
                }
            }

            if (validAuthors.Count == 0) return false;

            bool bookCreated = await BookRepository.Add(book);

            if (!bookCreated) return false;

            foreach(Guid guid in validAuthors)
            {
                bool relationshipCreated = !await AuthorBookRepository.AddBookToAuthor(new AuthorBook { AuthorId = guid, BookId = id });
                if (!relationshipCreated) return false;
            }

            return bookCreated;
        }

        public async Task<bool> Update(Guid id, string title, string isbn, string year)
        {
            Book book = await BookRepository.GetById(id);

            if (!string.IsNullOrEmpty(title))
                book.Title = title;
            if (!string.IsNullOrEmpty(isbn))
                book.ISBN = isbn;
            if (!string.IsNullOrEmpty(year))
                book.Year = year;

            return await BookRepository.Update(book);
        }

        public async Task<bool> Remove(Guid id)
        {
            Book book = await BookRepository.GetById(id);

            if (book == null)
                return false;
                
            return await BookRepository.Remove(book);
        }
    }
}
