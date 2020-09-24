using Biblioteca.Mappings;
using Biblioteca.Models;
using System;
using System.Collections.Generic;

namespace Biblioteca.Utils
{
    public static class Helpers
    {
        public static BookMapping ConvertBookToBookResponse(Book book)
        {
            BookMapping bookMapped = new BookMapping
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                Year = book.Year,
                Authors = new List<Guid>()
            };

            foreach (AuthorBook item in book.Authors)
            {
                bookMapped.Authors.Add(item.AuthorId);
            }
            
            return bookMapped;
        }

        public static AuthorMapping ConvertAuthorToAuthorResponse(Author author)
        {
            AuthorMapping authorMapped = new AuthorMapping {
                Id = author.Id,
                Name = author.Name,
                Lastname = author.Lastname,
                Email = author.Email,
                Birthday = author.Birthday,
                Books = new List<Guid>()
            };

            foreach (AuthorBook item in author.Books)
            {
                authorMapped.Books.Add(item.BookId);
            }

            return authorMapped;
        }
    }
}
