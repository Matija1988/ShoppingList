using Domain.ShopList;
using Domain.ShopListProducts;

namespace App.ShoppingLists.AddProductsToList;

internal sealed class AddProductsToListCommandHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider)
    : ICommandHandler<AddProductsToListCommand, int>
{
    public async Task<Result<int>> Handle(AddProductsToListCommand request, CancellationToken cancellationToken)
    {
        if(request.AddProductsToList.Products ==  null || request.AddProductsToList.Products.Count < 1)
        {
            return Result.Failure<int>(ShopListErrors.UpdateError());
        }

        var shopList = await context.ShopLists
           .Include(sl => sl.ShopListProducts)
           .FirstOrDefaultAsync(sl => sl.Id == request.AddProductsToList.shopListId, cancellationToken);

        if (shopList == null)
        {
            return Result.Failure<int>(ShopListErrors.NotFound(request.AddProductsToList.shopListId));
        }
        var shopListProducts = new List<ShopListProduct>();

        foreach (var productDto in request.AddProductsToList.Products)
        {
            var product = await context.Products
           .FirstOrDefaultAsync(p => p.Name == productDto.Name && p.UnitPrice == productDto.UnitPrice, cancellationToken);

            if (product == null)
            {
                product = new Product
                {
                    Name = productDto.Name,
                    UnitPrice = productDto.UnitPrice,
                    DateCreated = dateTimeProvider.UtcNow,
                    DateUpdated = dateTimeProvider.UtcNow,
                    IsActive = true
                };

                await context.Products.AddAsync(product, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }

            var shopListProduct = new ShopListProduct
            {
                ShopListId = shopList.Id,
                ProductId = product.Id,
                ProductQuantity = productDto.Quantity,
                TotalValue = productDto.Quantity * product.UnitPrice
            };

            shopListProducts.Add(shopListProduct);
        }

        await context.ShopListProducts.AddRangeAsync(shopListProducts, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(shopList.Id);
    }
}
