using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private Context Context { get; set; }

        public FriendRepository(Context context)
        {
            Context = context;
        }

        public async Task<List<Friend>> GetAll()
        {
            return await Context.FriendsDb.ToListAsync();
        }

        public async Task<List<Friend>> GetAllButMe(Guid id)
        {
            return await (Context.FriendsDb.Include(entity => entity.Friends).Where(entity => entity.Id != id)).ToListAsync();
        }

        public async Task<Friend> GetById(Guid Id)
        {
            return await Context.FriendsDb.Include(entity => entity.Friends).Include(entity => entity.State).Include(entity => entity.Country).FirstOrDefaultAsync(entity => entity.Id == Id);
        }

        public async Task<bool> Add(Friend friend)
        {
            try
            {
                await Context.FriendsDb.AddAsync(friend);
                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Friend friend)
        {
            try
            {
                Context.FriendsDb.Update(friend);
                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(Friend friend)
        {
            try
            {
                Context.FriendsDb.Remove(friend);
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
