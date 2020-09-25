using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IFriendRepository
    {
        Task<bool> Add(Friend friend);
        Task<bool> Delete(Friend friend);
        Task<List<Friend>> GetAll();
        Task<List<Friend>> GetAllButMe(Guid id);
        Task<Friend> GetById(Guid Id);
        Task<bool> Update(Friend friend);
    }
}