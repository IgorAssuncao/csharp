using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IFriendshipRepository
    {
        Task<bool> Add(Friendship friendship);
        Task<bool> Delete(Friendship friendship);
        Task<List<Friendship>> GetAll();
        Task<Friendship> GetById(Guid Id);
        Task<List<Friendship>> GetPersonFriends(Guid id);
        Task<bool> Update(Friendship friendship);
    }
}