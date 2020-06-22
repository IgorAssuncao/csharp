using System;

namespace Model
{
    public static class PersonExtension
    {
        public static string GetFullName(this Person person) => $"{person.Name} {person.Lastname}";

        public static int CalculatePersonNextBirthday(this Person person)
        {
            DateTime aux = new DateTime(DateTime.Now.Year, person.Birthday.Month, person.Birthday.Day);

            if (aux.Day.CompareTo(DateTime.Now.Day) < 0 && aux.Month.CompareTo(DateTime.Now.Month) <= 0)
                aux = aux.AddYears(1);

            return aux.Subtract(DateTime.Now).Days;
        }
    }
}
