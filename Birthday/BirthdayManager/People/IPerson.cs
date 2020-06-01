using System;

namespace Model
{
    interface IPerson
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthday { get; set; }

        public string GetFullName();

        public TimeSpan RemainingDaysForBirthday();
    }
}
