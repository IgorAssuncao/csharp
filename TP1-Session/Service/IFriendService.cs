using Models;
using System.Collections.Generic;

namespace Service
{
    public interface IFriendService
    {
        public List<Friend> GetFriends();
    }
}
