using Models;
using System.Collections.Generic;

namespace Repository
{
    public interface IFriendRepository
    {
        public List<Friend> GetFriends();
    }
}
