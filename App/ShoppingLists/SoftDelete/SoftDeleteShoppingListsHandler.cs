using Domain.ShopList;

namespace App.ShoppingLists.SoftDelete;

internal sealed class SoftDeleteShoppingListsHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider)
    : ICommandHandler<SoftDeleteShoppingListsCommand, int>
{
    public async Task<Result<int>> Handle(SoftDeleteShoppingListsCommand request, CancellationToken cancellationToken)
    {
        if (!request.ShopListIds.Any())
        {
            return Result.Failure<int>(ShopListErrors.UpdateError());
        }

        foreach (var shopId in request.ShopListIds)
        {
            var shopList = await context.ShopLists.FirstOrDefaultAsync(x => x.Id == shopId);

            if (shopList == null)
            {
                continue;
            }

            shopList.IsActive = false;
            shopList.DateUpdated = dateTimeProvider.UtcNow;
        }

        return await context.SaveChangesAsync(cancellationToken) > 0
               ? Result.Success<int>(request.ShopListIds.Count)
               : Result.Failure<int>(ProductErrors.UpdateError());
    }
}
