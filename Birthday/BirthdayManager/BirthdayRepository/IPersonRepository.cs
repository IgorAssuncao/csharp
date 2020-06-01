using Model;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IPersonRepository
    {
        public bool AddPerson(Person person, List<Person> personList);

        public Person GetPersonByName(string name, List<Person> personList);

        public TimeSpan GetPersonRemainingTimeForBirthday(Person person);
    }
}
