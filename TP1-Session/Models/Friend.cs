using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models
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

        public Friend(Guid _Id, string _Name, string _Lastname, string _Email, DateTime _Birthday)
        {
            Id = _Id;
            Name = _Name;
            Lastname = _Lastname;
            Email = _Email;
            Birthday = _Birthday;
        }
    }
}
