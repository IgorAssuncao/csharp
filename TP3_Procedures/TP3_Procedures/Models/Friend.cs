using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP3_Procedures.Models
{
    [Table("friend")]
    public class Friend
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }

        public Friend()
        {
        }

        public Friend(string name, string lastname, string email, DateTime birthday)
        {
            Name = name;
            Lastname = lastname;
            Email = email;
            Birthday = birthday;
        }
    }
}
