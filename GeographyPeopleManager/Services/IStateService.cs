using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IStateService
    {
        Task<bool> Add(StateRequest stateReq);
        Task<bool> Delete(Guid id);
        Task<List<State>> GetAll();
        Task<State> GetById(Guid id);
        Task<bool> Update(Guid id, State state);
    }
}