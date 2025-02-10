using Domain.Products;

namespace App.Abstractions.Data;
public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }

    DbSet<Product> Products { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
