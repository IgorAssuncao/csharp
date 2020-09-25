using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class StateRepository : IStateRepository
    {
        private Context Context { get; set; }

        public StateRepository(Context context)
        {
            Context = context;
        }

        public async Task<List<State>> GetAll()
        {
            return await Context.StateDb.ToListAsync();
        }

        public async Task<State> GetById(Guid Id)
        {
            return await Context.StateDb.Include(entity => entity.Friends).Include(entity => entity.Country).FirstOrDefaultAsync(entity => entity.Id == Id);
        }

        public async Task<bool> Add(State state)
        {
            try
            {
                await Context.StateDb.AddAsync(state);
                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(State state)
        {
            try
            {
                Context.StateDb.Update(state);
                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(State state)
        {
            try
            {
                Context.StateDb.Remove(state);
                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
