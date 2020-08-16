using Context;
using Models;
using Repository;
using System.Collections.Generic;

namespace Service
{
    public class FriendService : IFriendService
    {
        private IFriendRepository FriendRepository { get; set; }

        public FriendService(FriendContext context)
        {
            FriendRepository = new FriendRepository(context);           
        }

        public List<Friend> GetFriends()
        {
            return FriendRepository.GetFriends();
        }
    }
}
