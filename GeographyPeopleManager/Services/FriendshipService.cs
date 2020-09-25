using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FriendshipService : IFriendshipService
    {
        private IFriendshipRepository FriendshipRepository { get; set; }

        public FriendshipService(IFriendshipRepository friendshipRepository)
        {
            FriendshipRepository = friendshipRepository;
        }

        public async Task<List<Friendship>> GetAll()
        {
            return await FriendshipRepository.GetAll();
        }

        public async Task<Friendship> GetById(Guid id)
        {
            return await FriendshipRepository.GetById(id);
        }

        public async Task<List<Friendship>> GetPersonFriends(Guid id)
        {
            return await FriendshipRepository.GetPersonFriends(id);
        }

        public async Task<bool> Add(Friendship friendship)
        {
            await FriendshipRepository.Add(new Friendship() { PersonId = friendship.FriendId, FriendId = friendship.PersonId });
            return await FriendshipRepository.Add(friendship);
        }

        public async Task<bool> Delete(Friendship friendship)
        {
            return await FriendshipRepository.Delete(friendship);
        }
    }
}
