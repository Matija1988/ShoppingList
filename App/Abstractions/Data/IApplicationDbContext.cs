using Domain.Products;
using Domain.ShopList;
using Domain.ShopListProducts;

namespace App.Abstractions.Data;
public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<ShopList> ShopLists { get; set; }
    DbSet<ShopListProduct> ShopListProducts { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
