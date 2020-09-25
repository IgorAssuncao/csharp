using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IFriendService
    {
        Task<bool> Add(FriendRequest friendRequest);
        Task<bool> Delete(Guid id);
        Task<List<Friend>> GetAll();
        Task<List<Friend>> GetAllButMe(Guid id);
        Task<Friend> GetById(Guid id);
        Task<bool> Update(Guid id, Friend friend);
    }
}