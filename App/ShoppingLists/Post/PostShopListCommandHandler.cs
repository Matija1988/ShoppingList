using Domain.ShopList;
using Domain.ShopListProducts;

namespace App.ShoppingLists.Post
{
    internal sealed class PostShopListCommandHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider)
        : ICommandHandler<PostShopListCommand, int>
    {
        public async Task<Result<int>> Handle(PostShopListCommand request, CancellationToken cancellationToken)
        {
            if (request.ShopList.Products == null || request.ShopList.Products.Count < 1)
            {
                return Result.Failure<int>(ShopListErrors.PostError());
            }

            decimal totalValue = 0m;
            Product newProduct = new Product();

            foreach(var product in request.ShopList.Products)
            {
                newProduct.Name = product.Product.Name;
                newProduct.UnitPrice = product.Product.UnitPrice;
                newProduct.DateCreated = dateTimeProvider.UtcNow;
                newProduct.DateUpdated = dateTimeProvider.UtcNow;
                newProduct.IsActive = true;

                context.Products.Add(newProduct);
                await context.SaveChangesAsync(cancellationToken);
            }
            
            var shopList = new ShopList
            {
                Name = request.ShopList.Name,
                UserId = request.ShopList.Owner,
                DateCreated = dateTimeProvider.UtcNow,
                DateUpdated = dateTimeProvider.UtcNow,
                IsActive = true,
                ShopListProducts = new List<ShopListProduct>()
            };

            foreach (var product in request.ShopList.Products)
            {
                var shopListProduct = new ShopListProduct
                {
                    ProductId = newProduct.Id,
                    ProductQuantity = product.Quantity,
                    ShopListId = shopList.Id,
                };

                shopList.ShopListProducts.Add(shopListProduct);
            }

            context.ShopLists.Add(shopList);

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success(shopList.Id);
        }
    }
}

