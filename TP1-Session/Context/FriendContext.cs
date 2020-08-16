using Microsoft.EntityFrameworkCore;
using Models;

namespace Context
{
    public class FriendContext : DbContext
    {
        public FriendContext(DbContextOptions<FriendContext> options) : base(options)
        {
        }

        public DbSet<Friend> Friend { get; set; }
    }
}
