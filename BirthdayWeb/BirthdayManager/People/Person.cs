using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("person")]
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthday { get; set; }

        public Person() { }

        public Person(int id, string name, string lastname, DateTime birthday)
        {
            Id = id;
            Name = name;
            Lastname = lastname;
            Birthday = birthday;
        }

        public override string ToString() => $"{this.Name} {this.Lastname} - {this.Birthday}";
    }
}
