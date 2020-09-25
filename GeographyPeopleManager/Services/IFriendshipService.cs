using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IFriendshipService
    {
        Task<bool> Add(Friendship friendship);
        Task<bool> Delete(Friendship friendship);
        Task<Friendship> GetById(Guid id);
        Task<List<Friendship>> GetPersonFriends(Guid id);
        Task<List<Friendship>> GetAll();
    }
}