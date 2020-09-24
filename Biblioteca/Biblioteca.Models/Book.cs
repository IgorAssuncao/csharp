using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models
{
    [Table("Book")]
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Year { get; set; }
        public virtual List<AuthorBook> Authors { get; set; }
    }
}
