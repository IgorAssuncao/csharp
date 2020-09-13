using Microsoft.EntityFrameworkCore;
using TP3_Auth.Models;

namespace TP3_Auth.Context
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Friend> Friend { get; set; }
    }
}
