using System;
using System.Collections.Generic;

namespace Model
{
    public class State
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public virtual List<Friend> Friends { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
    }
}
