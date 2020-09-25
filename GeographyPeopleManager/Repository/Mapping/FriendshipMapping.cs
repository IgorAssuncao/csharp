using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model;

namespace Repository.Mapping
{
    public class FriendshipMapping : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.ToTable("Friendship");

            builder.HasKey(entity => new { entity.PersonId, entity.FriendId });
            builder.HasIndex(entity => entity.PersonId);

            builder.HasOne(entity => entity.Friend)
                .WithMany(entity => entity.Friends)
                .HasForeignKey(entity => entity.PersonId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(entity => entity.Friend)
                .WithMany(entity => entity.Friends)
                .HasForeignKey(entity => entity.FriendId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
