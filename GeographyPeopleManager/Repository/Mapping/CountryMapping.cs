using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model;

namespace Repository.Mapping
{
    public class CountryMapping : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Country");

            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Name).IsRequired();
            builder.Property(entity => entity.PhotoUrl).IsRequired();

            builder.HasMany(entity => entity.Friends)
                .WithOne(entity => entity.Country)
                .HasForeignKey(entity => entity.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(entity => entity.States)
                .WithOne(entity => entity.Country)
                .HasForeignKey(entity => entity.CountryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
