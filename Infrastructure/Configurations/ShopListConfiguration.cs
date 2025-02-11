using Domain.ShopList;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class ShopListConfiguration : IEntityTypeConfiguration<ShopList>
{
    public void Configure(EntityTypeBuilder<ShopList> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
              .HasMaxLength(50)
              .IsRequired();

        builder.Property(x => x.DateCreated)
               .IsRequired();

        builder.Property(x => x.IsActive)
               .HasDefaultValue(true);

        builder.Property(x => x.TotalValue)
               .HasColumnType("decimal(18,2)")
               .HasDefaultValue(0);

        builder.HasOne(x => x.User)
               .WithMany()
               .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.ShopListProducts)
               .WithOne(x => x.ShopList)
               .HasForeignKey(x => x.ShopListId);
    }
}
