using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class StateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IFormFile PhotoUrl { get; set; }
        public virtual List<Friend> Friends { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
    }
}
