using System;
using System.ComponentModel.DataAnnotations;

namespace TP3_Auth.Models
{
    public class Friend
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }

        public Friend()
        {
        }

        public Friend(string name, string lastname, string email, string password, DateTime birthday)
        {
            Name = name;
            Lastname = lastname;
            Email = email;
            Password = password;
            Birthday = birthday;
        }
    }
}
