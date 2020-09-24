using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models
{
    [Table("AuthorBook")]
    public class AuthorBook
    {
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }
}
