using CommonPersonStatus;
using Microsoft.Extensions.Configuration;
using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public static class PersonService
    {
        public static PersonRepository personRepository { get; set; }

        public static List<Person> GetAllPeople()
        {
            return personRepository.GetAllPeople();
        }

        public static PersonFound GetById(int Id)
        {
            try
            {
                Person person = personRepository.GetPersonById(Id);

                PersonFound personStatus;

                personStatus = person != null ? new PersonFound { Found = true, Person = person, Message = "Person Found!" } : throw new Exception("Person not found");

                personStatus.RemainingDaysForBirthday = person.CalculatePersonNextBirthday();

                return personStatus;
            }
            catch (Exception exception)
            {
                return new PersonFound { Found = false, Message = exception.Message };
            }
        }

        public static List<Person> SearchByNameOrLastname(string text)
        {
            return personRepository.SearchByNameOrLastname(text);
        }

        public static List<Person> GetAllPeopleThatBirthdayIsToday()
        {
            return personRepository.GetAllPeopleThatBirthdayIsToday();
        }

        public static PersonAdded Add(string name, string lastname, DateTime birthday)
        {
            try
            {
                if (name == "")
                    throw new Exception("Name is empty");

                if (lastname == "")
                    throw new Exception("Lastame is empty");

                Person person = new Person(0, name, lastname, birthday);

                personRepository.AddPerson(person);

                PersonAdded personStatus = new PersonAdded { Registered = true, Message = "Person Registered" };
                return personStatus;
            }
            catch (Exception exception)
            {
                return new PersonAdded { Registered = false, Message = exception.Message };
            }
        }

        public static void UpdatePerson(int PersonId, string Name, string Lastname, DateTime Birthday)
        {
            Person person = GetById(PersonId).Person;

            person.Name = Name;
            person.Lastname = Lastname;
            person.Birthday = Birthday;

            personRepository.UpdatePerson(person);
        }

        public static void DeletePerson(int PersonId)
        {
            Person person = personRepository.GetPersonById(PersonId);
            personRepository.DeletePerson(person);
        }
    }
}

