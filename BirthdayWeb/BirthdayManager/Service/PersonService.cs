using CommonPersonStatus;
using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public static class PersonService
    {
        public static List<Person> GetAllPeople()
        {
            return PersonRepositoryWrapper.GetAllPeople();
        }

        public static PersonFound GetById(int Id)
        {
            try
            {
                Person person = PersonRepositoryWrapper.GetPersonById(Id);

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
            return PersonRepositoryWrapper.SearchByNameOrLastname(text);
        }

        public static List<Person> GetPersonFriends(int id)
        {
            return PersonRepositoryWrapper.GetPersonFriends(id);
        }

        public static List<Person> GetAllPeopleThatBirthdayIsToday()
        {
            return PersonRepositoryWrapper.GetAllPeopleThatBirthdayIsToday();
        }

        public static PersonAdded Add(string name, string lastname, DateTime birthday)
        {
            try
            {
                if (name == "")
                    throw new Exception("Name is empty");

                if (lastname == "")
                    throw new Exception("Lastame is empty");

                Person person = new Person(name, lastname, birthday);

                bool addPersonResult = PersonRepositoryWrapper.AddPerson(person);

                PersonAdded personStatus = new PersonAdded { Registered = addPersonResult };

                personStatus.Message = personStatus.Registered ? "Person Registered" : "Person not Registered";

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

            PersonRepositoryWrapper.UpdatePerson(person);
        }

        public static void DeletePerson(int PersonId)
        {
            Person person = PersonRepositoryWrapper.GetPersonById(PersonId);
            PersonRepositoryWrapper.DeletePerson(person);
        }

        public static void AddPersonFriend(int PersonId, int FriendId)
        {
            Person person = PersonRepositoryWrapper.GetPersonById(PersonId);
            PersonRepositoryWrapper.AddFriend(person, FriendId);

            Person friend = PersonRepositoryWrapper.GetPersonById(FriendId);
            PersonRepositoryWrapper.AddFriend(friend, PersonId);
        }

        public static void RemovePersonFriend(int PersonId, int FriendId)
        {
            Person person = PersonRepositoryWrapper.GetPersonById(PersonId);
            PersonRepositoryWrapper.RemoveFriend(person, FriendId);

            Person friend = PersonRepositoryWrapper.GetPersonById(FriendId);
            PersonRepositoryWrapper.RemoveFriend(friend, PersonId);
        }
    }
}

