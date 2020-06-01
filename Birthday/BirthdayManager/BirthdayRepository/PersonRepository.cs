using System;
using System.Collections.Generic;
using Model;

namespace Repository
{
    public static class PersonRepository
    {
        public static IPersonRepository _personRepository = new PersonRepositoryWrapper();

        public static List<Person> personList = new List<Person>();

        public static bool AddPerson(Person person)
        {
            return _personRepository.AddPerson(person, personList);
        }

        public static Person GetPersonByName(string name)
        {
            return _personRepository.GetPersonByName(name, personList);
        }

        public static TimeSpan GetPersonRemainingTimeForBirthday(Person person)
        {
            return _personRepository.GetPersonRemainingTimeForBirthday(person);
        }
    }
}
