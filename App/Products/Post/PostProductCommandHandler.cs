
namespace App.Products.Post;

internal class PostProductCommandHandler(IApplicationDbContext context)
    : ICommandHandler<PostProductCommand, int>
{
    public async Task<Result<int>> Handle(PostProductCommand command, CancellationToken cancellationToken)
    {
        List<Product> products = new List<Product>();

        foreach (var item in command.Products)
        {
            if(await context.Products.AnyAsync(x => x.Name == item.Name && x.UnitPrice == item.UnitPrice, cancellationToken: cancellationToken))
            {
                continue;
            }
            else if (await context.Products.AnyAsync(x => x.Name == item.Name && x.UnitPrice != item.UnitPrice && x.DateUpdated != DateTime.Now, cancellationToken: cancellationToken))
            {
                var product = await context.Products.SingleOrDefaultAsync(x => x.Name == item.Name);

                product.UnitPrice = item.UnitPrice;
                product.DateUpdated = DateTime.Now;

                context.Products.Update(product);
                await context.SaveChangesAsync(cancellationToken);
            }
            else 
            {
                products.Add(new Product
                {
                    Name = item.Name,
                    UnitPrice = item.UnitPrice,
                    DateUpdated = DateTime.Now,
                    DateCreated = DateTime.Now,
                    IsActive = true,
                });          
            }
        }

        await context.Products.AddRangeAsync(products, cancellationToken);
        return products is null 
            ? Result.Failure<int>(ProductErrors.ProductsInDb()) 
            : await context.SaveChangesAsync(cancellationToken);
    }
}
