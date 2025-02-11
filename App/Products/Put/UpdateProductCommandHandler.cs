namespace App.Products.Put;

internal sealed class UpdateProductCommandHandler(IApplicationDbContext context)
    : ICommandHandler<UpdateProductCommand, int>
{
    public async Task<Result<int>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        if(request.Products == null || request.Products.Count < 1)
        {
            return Result.Failure<int>(ProductErrors.BatchRequestEmpty());
        }

        foreach (var item in request.Products) 
        { 
            var product = await context.Products.FindAsync(item.Id);

            if(product == null)
            {
                continue;
            }

            product.Name = item.Name;
            product.UnitPrice = item.UnitPrice; 
            product.IsActive = item.IsActive;
            product.DateUpdated = DateTime.UtcNow;
        }

        return await context.SaveChangesAsync(cancellationToken) > 0
                   ? Result.Success<int>(request.Products.Count)
                   : Result.Failure<int>(ProductErrors.UpdateError());
    }
}
