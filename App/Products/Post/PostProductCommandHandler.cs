using MediatR;

namespace App.Products.Post;

/// <summary>
/// Dodavanje liste proizvoda u bazu podataka.
/// </summary>
/// <param name="context"></param>

internal sealed class PostProductCommandHandler(IApplicationDbContext context)
    : ICommandHandler<PostProductCommand, int>
{
    /// <summary>
    /// Metoda u petlji provjerava postoji li proizvod sa istim nazivom i cijenom, radi izbjegavanja duplikacije, ako postoji preskace.
    /// Ako postoji proizvod sa istim nazivom no razlicitom cijenom i razlika u vremenu od zadnjeg unosa je veca od 12 sati postojeci proizvod
    /// se modificira. 
    /// Ako gore navedeni uvijeti nisu ispunjeni proizvodi se dodaju u novu kolekciju koja se dodaje u bazu podataka. 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result<int>> Handle(PostProductCommand request, CancellationToken cancellationToken)
    {
        if (request.Products == null || request.Products.Count < 1)
        {
            return Result.Failure<int>(ProductErrors.BatchRequestEmpty());
        }

        List<Product> products = new List<Product>();

        foreach (var item in request.Products)
        {
            if(await context.Products.AnyAsync(x => x.Name == item.Name && x.UnitPrice == item.UnitPrice, cancellationToken: cancellationToken))
            {
                continue;
            }
            else if (await context.Products.AnyAsync(x => x.Name == item.Name && x.UnitPrice != item.UnitPrice && x.DateUpdated <= DateTime.Now.AddHours(-12), cancellationToken: cancellationToken))
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
