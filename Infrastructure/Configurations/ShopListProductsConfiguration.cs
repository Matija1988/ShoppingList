using Domain.ShopListProducts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class ShopListProductsConfiguration : IEntityTypeConfiguration<ShopListProduct>
{
    public void Configure(EntityTypeBuilder<ShopListProduct> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ToTable(tb => tb.HasTrigger("trg_UpdateListProductTotalValue"));
    }
}
