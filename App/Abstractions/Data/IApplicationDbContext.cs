using Domain.Products;
using Domain.ShopList;

namespace App.Abstractions.Data;
public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<ShopList> ShopLists { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
