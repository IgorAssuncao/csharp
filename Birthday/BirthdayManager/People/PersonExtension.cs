using System;
using System.Runtime.CompilerServices;

namespace Model
{
    public static class PersonExtension
    {
        public static string GetFullName(this Person person) => $"{person.Name} {person.Lastname}";

        public static TimeSpan CalculatePersonBirthday(this Person person)
        {
            DateTime aux = new DateTime(DateTime.Now.Year, person.Birthday.Month, person.Birthday.Day);

            if (aux.Day.CompareTo(DateTime.Now.Day) < 0 && aux.Month.CompareTo(DateTime.Now.Month) <= 0)
                aux = aux.AddYears(1);

            return aux.Subtract(DateTime.Now);
        }

        public static void AddFriend(this Person person, int friendId)
        {
            person.Friends.Add(new PersonFriends(person.Id, friendId));
        }

        public static void RemoveFriend(this Person person, int friendId)
        {
            person.Friends.Remove(person.Friends.Find(friend => friend.friendId == friendId));
        }
    }
}
