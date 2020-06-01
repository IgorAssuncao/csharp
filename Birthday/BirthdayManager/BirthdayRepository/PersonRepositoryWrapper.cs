using Model;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class PersonRepositoryWrapper : IPersonRepository
    {
        public bool AddPerson(Person person, List<Person> personList)
        {
            try
            {
                personList.Add(person);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Person GetPersonByName(string name, List<Person> personList)
        {
            return personList.Find(person => person.Name == name);
        }

        public TimeSpan GetPersonRemainingTimeForBirthday(Person person)
        {
            return person.RemainingDaysForBirthday();
        }
    }
}
