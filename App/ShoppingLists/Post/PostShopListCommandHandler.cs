using Domain.ShopList;
using Domain.ShopListProducts;

namespace App.ShoppingLists.Post
{
    internal sealed class PostShopListCommandHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider)
        : ICommandHandler<PostShopListCommand, List<int>>
    {
        public async Task<Result<List<int>>> Handle(PostShopListCommand request, CancellationToken cancellationToken)
        {
            if (request.ShopLists == null || request.ShopLists.Count < 1)
            {
                return Result.Failure<List<int>>(ShopListErrors.PostError());
            }

            var createdShopListIds = new List<int>();

            foreach (var shopListDto in request.ShopLists)
            {
                if (shopListDto.Products == null || shopListDto.Products.Count < 1)
                {
                    continue;
                }

                var shopList = new ShopList
                {
                    Name = shopListDto.Name,
                    UserId = shopListDto.Owner,
                    DateCreated = dateTimeProvider.UtcNow,
                    DateUpdated = dateTimeProvider.UtcNow,
                    IsActive = true,
                    ShopListProducts = new List<ShopListProduct>()
                };

                context.ShopLists.Add(shopList);
                await context.SaveChangesAsync(cancellationToken);

                foreach (var productDto in shopListDto.Products)
                {
                    var existingProduct = await context.Products
                        .FirstOrDefaultAsync(p => p.Name == productDto.Product.Name && p.UnitPrice == productDto.Product.UnitPrice, cancellationToken);

                    if (existingProduct == null)
                    {
                        existingProduct = new Product
                        {
                            Name = productDto.Product.Name,
                            UnitPrice = productDto.Product.UnitPrice,
                            DateCreated = dateTimeProvider.UtcNow,
                            DateUpdated = dateTimeProvider.UtcNow,
                            IsActive = true
                        };

                        context.Products.Add(existingProduct);
                        await context.SaveChangesAsync(cancellationToken);
                    }

                    var shopListProduct = new ShopListProduct
                    {
                        ShopListId = shopList.Id,
                        ProductId = existingProduct.Id,
                        ProductQuantity = productDto.Quantity,
                        TotalValue = productDto.Quantity * existingProduct.UnitPrice
                    };

                    shopList.ShopListProducts.Add(shopListProduct);
                }

                await context.SaveChangesAsync(cancellationToken);
                createdShopListIds.Add(shopList.Id);
            }

            return Result.Success(createdShopListIds);
        }
    }
}

