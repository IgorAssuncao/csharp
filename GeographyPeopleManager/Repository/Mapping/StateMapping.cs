using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model;

namespace Repository.Mapping
{
    public class StateMapping : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("State");

            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Name).IsRequired();
            builder.Property(entity => entity.PhotoUrl).IsRequired();

            builder.HasMany(entity => entity.Friends)
                .WithOne(entity => entity.State)
                .HasForeignKey(entity => entity.StateId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(entity => entity.Country)
                .WithMany(entity => entity.States)
                .HasForeignKey(entity => entity.CountryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
