using System;
using System.Collections.Generic;
using Model;

namespace Repository
{
    public static class PersonRepositoryWrapper
    {
        public static IPersonRepository _personRepository = new PersonRepository();

        public static List<Person> GetAllPeople()
        {
            return _personRepository.GetAllPeople();
        }

        public static Person GetPersonById(int Id)
        {
            return _personRepository.GetPersonById(Id);
        }

        public static List<Person> SearchByNameOrLastname(string text)
        {
            return _personRepository.SearchByNameOrLastname(text);
        }

        public static List<Person> GetPersonFriends(int id)
        {
            return _personRepository.GetPersonFriends(id);
        }

        public static List<Person> GetAllPeopleThatBirthdayIsToday()
        {
            return _personRepository.GetAllPeopleThatBirthdayIsToday();
        }

        public static bool AddPerson(Person person)
        {
            try
            {
                _personRepository.AddPerson(person);
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static void UpdatePerson(Person person)
        {
            _personRepository.UpdatePerson(person);
        }

        public static void DeletePerson(Person person)
        {
            _personRepository.DeletePerson(person);
        }

        public static void AddFriend(Person person, int FriendId)
        {
            _personRepository.AddFriend(person, FriendId);
        }

        public static void RemoveFriend(Person person, int FriendId)
        {
            _personRepository.RemoveFriend(person, FriendId);
        }
    }
}
