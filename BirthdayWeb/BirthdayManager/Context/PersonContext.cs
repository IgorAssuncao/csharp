using Microsoft.EntityFrameworkCore;
using Model;

namespace Context
{
    public class PersonContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<PersonFriends> PersonFriends { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Filename = birthdayManager.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Person>().HasKey(m => m.Id);
            builder.Entity<Person>().HasMany<Person>();
            builder.Entity<PersonFriends>().HasKey(m => m.Id);
            base.OnModelCreating(builder);
        }
    }
}
