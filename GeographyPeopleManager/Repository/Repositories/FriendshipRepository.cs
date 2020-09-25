using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class FriendshipRepository : IFriendshipRepository
    {
        private Context Context { get; set; }

        public FriendshipRepository(Context context)
        {
            Context = context;
        }

        public async Task<List<Friendship>> GetAll()
        {
            return await Context.FriendshipDb.ToListAsync();
        }

        public async Task<Friendship> GetById(Guid Id)
        {
            return await Context.FriendshipDb.FindAsync(Id);
        }

        public async Task<List<Friendship>> GetPersonFriends(Guid id)
        {
            return await Context.FriendshipDb
                .Include(entity => entity.Friend)
                .Where(entity => entity.PersonId == id || entity.FriendId != id)
                .ToListAsync();
        }

        public async Task<bool> Add(Friendship friendship)
        {
            try
            {
                await Context.FriendshipDb.AddAsync(friendship);
                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Friendship friendship)
        {
            try
            {
                Context.FriendshipDb.Update(friendship);
                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(Friendship friendship)
        {
            try
            {
                Context.FriendshipDb.Remove(friendship);
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
