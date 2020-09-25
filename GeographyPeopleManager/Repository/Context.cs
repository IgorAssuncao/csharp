using Microsoft.EntityFrameworkCore;
using Model;
using Repository.Mapping;

namespace Repository
{
    public class Context : DbContext
    {
        public DbSet<Friend> FriendsDb { get; set; }
        public DbSet<Friendship> FriendshipDb { get; set; }
        public DbSet<State> StateDb { get; set; }
        public DbSet<Country> CountryDb { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GeographyFriendshipManager;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FriendMapping());
            modelBuilder.ApplyConfiguration(new FriendshipMapping());
            modelBuilder.ApplyConfiguration(new StateMapping());
            modelBuilder.ApplyConfiguration(new CountryMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
