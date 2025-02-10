namespace App.Products.GetById;

internal sealed class GetProductByIdQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetProductByIdQuery, ProductResponse>
{
    public async Task<Result<ProductResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        ProductResponse? response = await context.Products
            .Where(e => e.Id == query.Id)
            .Select(e => new ProductResponse
            {
                Id = e.Id,
                Name = e.Name,
                UnitPrice = e.UnitPrice,
                DateUpdated = e.DateUpdated,
            })
            .SingleOrDefaultAsync(cancellationToken);

        return response is not null
            ? Result.Success(response)
            : Result.Failure<ProductResponse>(ProductErrors.NotFound(query.Id));
    }
}
