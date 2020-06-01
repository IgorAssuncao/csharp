using CommonPersonStatus;
using Model;
using Repository;
using System;

namespace Service
{
    public static class PersonService
    {
        public static PersonAdded Add(string name, string lastname, DateTime birthday)
        {
            try
            {
                if (name == "")
                    throw new Exception("Name is empty");

                if (lastname == "")
                    throw new Exception("Lastame is empty");

                Person person = new Person(name, lastname, birthday);

                bool addPersonResult = PersonRepository.AddPerson(person);

                PersonAdded personStatus = new PersonAdded { Registered = addPersonResult };

                personStatus.Message = personStatus.Registered ? "Person Registered" : "Person not Registered";

                return personStatus;
            }
            catch (Exception exception)
            {
                return new PersonAdded { Registered = false, Message = exception.Message };
            }
        }

        public static PersonFound GetByName(string name)
        {
            try
            {
                if (name == "")
                    throw new Exception("Name is empty");

                Person person = PersonRepository.GetPersonByName(name);

                PersonFound personStatus;

                personStatus = person != null ? new PersonFound { Found = true, Person = person, Message = "Person Found!" } : throw new Exception("Person not found");

                personStatus.RemainingTimeForBirthday = CalculatePersonBirthday(personStatus.Person);

                return personStatus;
            }
            catch (Exception exception)
            {
                return new PersonFound { Found = false, Message = exception.Message };
            }
        }

        private static TimeSpan CalculatePersonBirthday(Person person)
        {
            return person.RemainingDaysForBirthday();
        }
    }
}

