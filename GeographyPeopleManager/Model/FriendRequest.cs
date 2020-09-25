using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Model
{
    public class FriendRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public IFormFile PhotoURL { get; set; }
        public List<Friendship> Friends { get; set; }
        public Country Country { get; set; }
        public Guid CountryId { get; set; }
        public State State { get; set; }
        public Guid StateId { get; set; }
    }
}
