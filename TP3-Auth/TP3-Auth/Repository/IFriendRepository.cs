using System;
using System.Collections.Generic;
using TP3_Auth.Models;

namespace TP3_Auth.Repository
{
    public interface IFriendRepository
    {
        bool CreateFriend(Friend friend);
        bool DeleteFriend(Guid id);
        Friend Find(Guid id);
        Friend Find(string email);
        List<Friend> FindAll();
        bool UpdateFriend(Guid id, Friend friend);
    }
}