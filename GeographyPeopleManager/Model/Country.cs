using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Country
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public virtual List<Friend> Friends { get; set; }
        public virtual List<State> States { get; set; }
    }
}
