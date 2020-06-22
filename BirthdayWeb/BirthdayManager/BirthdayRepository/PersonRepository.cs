using System;
using System.Linq;

using Model;
using Context;
using System.Collections.Generic;

namespace Repository
{
    public class PersonRepository : IPersonRepository
    {
        PersonContext PersonDb = new PersonContext();

        public List<Person> GetAllPeople()
        {
            try
            {
                return PersonDb.People.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Person>();
            }
        }

        public Person GetPersonById(int Id)
        {
            try
            {
                return PersonDb.People.Find(Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public List<Person> SearchByNameOrLastname(string text)
        {
            try
            {
                return PersonDb.People.Where(person => person.Name.Contains(text) || person.Lastname.Contains(text)).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Person>();
            }
        }

        public List<Person> GetPersonFriends(int id)
        {
            Person person = PersonDb.People.Find(id);

            List<Person> friends = new List<Person>();

            foreach(PersonFriends personFriend in person.Friends)
            {
                friends.Add(PersonDb.People.Find(personFriend.Id));
            }

            return friends;
        }

        public List<Person> GetAllPeopleThatBirthdayIsToday()
        {
            try
            {
                List<Person> allPeople = PersonDb.People.ToList();

                allPeople = allPeople.FindAll(person => person.CalculatePersonNextBirthday() == 0);

                return allPeople;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Person>();
            }
        }

        public void AddPerson(Person person)
        {
            PersonDb.Add(person);
            PersonDb.SaveChanges();
        }

        public void UpdatePerson(Person person)
        {
            PersonDb.Update(person);
            PersonDb.SaveChanges();
        }

        public void DeletePerson(Person person)
        {
            try
            {
                PersonDb.Remove(person);
                PersonDb.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void AddFriend(Person person, int friendId)
        {
            try
            {
                person.AddFriend(friendId);
                PersonDb.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void RemoveFriend(Person person, int friendId)
        {
            try
            {
                person.RemoveFriend(friendId);
                PersonDb.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

    }
}
