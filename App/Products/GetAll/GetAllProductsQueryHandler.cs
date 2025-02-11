namespace App.Products.GetAll;

internal sealed class GetAllProductsQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetAllProductsQuery, List<ProductResponse>>
{
    public async Task<Result<List<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var response = await context.Products
            .Select(x => new ProductResponse
            {
                Id = x.Id,
                Name = x.Name,
                UnitPrice = x.UnitPrice,
                DateUpdated = x.DateUpdated,
            })
            .ToListAsync(cancellationToken);

        return response;
    }
}
