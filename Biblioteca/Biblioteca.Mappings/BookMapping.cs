using System;
using System.Collections.Generic;

namespace Biblioteca.Mappings
{
    public class BookMapping
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Year { get; set; }
        public List<Guid> Authors { get; set; }
    }
}
