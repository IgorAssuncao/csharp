using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model;

namespace Repository.Mapping
{
    public class FriendMapping : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.ToTable("Friend");

            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Name).IsRequired();
            builder.Property(entity => entity.Lastname).IsRequired();
            builder.Property(entity => entity.Email).IsRequired();
            builder.Property(entity => entity.Phone).IsRequired();
            builder.Property(entity => entity.Birthday).IsRequired();
            builder.Property(entity => entity.PhotoURL).IsRequired();

            builder.HasOne(entity => entity.State)
                .WithMany(entity => entity.Friends)
                .HasForeignKey(entity => entity.StateId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(entity => entity.Country)
                .WithMany(entity => entity.Friends)
                .HasForeignKey(entity => entity.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(entity => entity.Friends)
                .WithOne(entity => entity.Friend)
                .HasForeignKey(entity => entity.PersonId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(entity => entity.Friends)
                .WithOne(entity => entity.Friend)
                .HasForeignKey(entity => entity.FriendId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
