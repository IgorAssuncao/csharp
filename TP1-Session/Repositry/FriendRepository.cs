using System.Collections.Generic;
using Models;
using Context;
using System.Linq;
using System;

namespace Repository
{
    public class FriendRepository : IFriendRepository
    {
        public FriendContext FriendDb { get; set; }

        public FriendRepository(FriendContext friendContext)
        {
            FriendDb = friendContext;
        }

        public List<Friend> GetFriends()
        {
            try
            {
                List<Friend> friends = FriendDb.Friend.ToList();
                return friends;
            } catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Friend>();
            }
        }
    }
}
