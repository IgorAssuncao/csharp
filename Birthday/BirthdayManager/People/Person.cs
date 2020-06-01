using System;

namespace Model
{
    public class Person : IPerson
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthday { get; set; }

        public Person(string name, string lastname, DateTime birthday)
        {
            Name = name;
            Lastname = lastname;
            Birthday = birthday;
        }

        public string GetFullName() => $"{this.Name} {this.Lastname}";

        public override string ToString() => $"{this.GetFullName()} - {this.Birthday}";

        public TimeSpan RemainingDaysForBirthday()
        {
            DateTime aux = new DateTime(DateTime.Now.Year, Birthday.Month, Birthday.Day);

            var aux1 = aux.Day.CompareTo(DateTime.Now.Day);
            var aux2 = aux.Month.CompareTo(DateTime.Now.Month);

            if (aux.Day.CompareTo(DateTime.Now.Day) < 0 && aux.Month.CompareTo(DateTime.Now.Month) <= 0)
                aux = aux.AddYears(1);

            return aux.Subtract(DateTime.Now);
        }
    }
}
