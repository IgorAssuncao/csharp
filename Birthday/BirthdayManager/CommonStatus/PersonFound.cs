using System;
using Model;

namespace CommonPersonStatus
{
    public class PersonFound
    {
        public bool Found { get; set; }
        public string Message { get; set; }
        public Person Person { get; set; }
        public int RemainingDaysForBirthday { get; set; }
    }
}
