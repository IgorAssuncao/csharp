using System;
using System.Collections.Generic;
using System.Linq;
using TP3_Auth.Context;
using TP3_Auth.Models;

namespace TP3_Auth.Repository
{
    public class FriendRepository : IFriendRepository
    {
        private ApplicationContext ApplicationContext { get; set; }

        public FriendRepository(ApplicationContext applicationContext)
        {
            ApplicationContext = applicationContext;
        }

        public List<Friend> FindAll()
        {
            return ApplicationContext.Friend.ToList();
        }

        public Friend Find(Guid id)
        {
            return ApplicationContext.Friend.Find(id);
        }

        public Friend Find(string email)
        {
            return ApplicationContext.Friend.FirstOrDefault(friend => friend.Email == email);
        }

        public bool CreateFriend(Friend friend)
        {
            try
            {
                friend.Id = Guid.NewGuid();
                ApplicationContext.Friend.Add(friend);
                ApplicationContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool UpdateFriend(Guid id, Friend friend)
        {
            try
            {
                Friend friendFound = Find(id);
                if (friendFound == null)
                    return false;

                Friend newFriend = new Friend();

                newFriend.Name = string.IsNullOrEmpty(friend.Name) ? friendFound.Name : friend.Name;
                newFriend.Lastname = string.IsNullOrEmpty(friend.Lastname) ? friendFound.Lastname : friend.Lastname;
                newFriend.Email = string.IsNullOrEmpty(friend.Email) ? friendFound.Email : friend.Email;
                newFriend.Password = string.IsNullOrEmpty(friend.Password) ? friendFound.Password : friend.Password;
                newFriend.Birthday = friend.Birthday.CompareTo(new DateTime(0001, 1, 1)) == 0 ? friendFound.Birthday : friend.Birthday;

                ApplicationContext.Friend.Update(newFriend);
                ApplicationContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteFriend(Guid id)
        {
            try
            {
                Friend friendFound = Find(id);
                if (friendFound == null)
                    return false;

                ApplicationContext.Friend.Remove(friendFound);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
