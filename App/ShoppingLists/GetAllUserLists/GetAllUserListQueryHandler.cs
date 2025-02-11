using App.Abstractions.Authentication;
using Domain.ShopList;

namespace App.ShoppingLists.GetAllUserLists;

internal sealed class GetAllUserListQueryHandler(IApplicationDbContext context, IUserContext userContext)
    : IQueryHandler<GetAllUserListQuery, List<ShopListReadResponse>>
{
    public async Task<Result<List<ShopListReadResponse>>> Handle(GetAllUserListQuery request, CancellationToken cancellationToken)
    {
        if (request.userId != userContext.UserId) 
        {
            return Result.Failure<List<ShopListReadResponse>>(UserErrors.Unauthorized());
        }

        var shopList = await context.ShopLists
            .Include(x => x.User)
            .Include(x => x.ShopListProducts)
            .ThenInclude(x => x.Product)
            .Where(x => x.User.Id == userContext.UserId)
            .Select(x => new ShopListReadResponse
            {
                ShopListId = x.Id,
                ShopListName = x.Name,
                ShopListTotalValue = x.TotalValue,
                DateUpdated = x.DateUpdated,
                Products = x.ShopListProducts.Select(p => new ShopListProductResponse
                {
                    Product = new ProductResponse
                    {
                        Id = p.Product.Id,
                        UnitPrice = p.Product.UnitPrice,
                        DateUpdated = p.Product.DateUpdated,
                        Name = p.Product.Name,
                    },
                    ProductQuantity = p.ProductQuantity,
                    TotalValue = p.TotalValue,
                }).ToList()
            })
            .AsNoTracking()
            .ToListAsync();

        return shopList is null 
            ? Result.Failure<List<ShopListReadResponse>>(ShopListErrors.NoUserList(userContext.UserId))
            : Result.Success<List<ShopListReadResponse>>(shopList);
    }
}
