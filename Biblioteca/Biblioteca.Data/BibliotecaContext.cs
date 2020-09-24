using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data
{
    public class BibliotecaContext : DbContext
    {
        // public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options) { }

        public DbSet<Author> AuthorSet { get; set; }
        public DbSet<Book> BookSet { get; set; }
        public DbSet<AuthorBook> AuthorBookSet { get;set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Biblioteca;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorBook>().HasKey(entity => new { entity.AuthorId, entity.BookId });
            modelBuilder.Entity<AuthorBook>()
                .HasOne<Author>(author => author.Author)
                .WithMany(author => author.Books)
                .HasForeignKey(entity => entity.AuthorId);
            modelBuilder.Entity<AuthorBook>()
                .HasOne<Book>(book => book.Book)
                .WithMany(book => book.Authors)
                .HasForeignKey(entity => entity.BookId);
        }
    }
}
