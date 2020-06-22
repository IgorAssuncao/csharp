using Model;
using System.Collections.Generic;

namespace Repository
{
    public interface IPersonRepository
    {
        public List<Person> GetAllPeople();

        public Person GetPersonById(int Id);

        public List<Person> SearchByNameOrLastname(string text);

        public List<Person> GetPersonFriends(int id);

        public List<Person> GetAllPeopleThatBirthdayIsToday();

        public void AddPerson(Person Person);

        public void UpdatePerson(Person person);

        public void DeletePerson(Person person);

        public void AddFriend(Person person, int FriendId);

        public void RemoveFriend(Person person, int FriendId);
    }
}
