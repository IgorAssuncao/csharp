using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IStateRepository
    {
        Task<bool> Add(State state);
        Task<bool> Delete(State state);
        Task<List<State>> GetAll();
        Task<State> GetById(Guid Id);
        Task<bool> Update(State state);
    }
}