using Domain.ShopList;

namespace App.ShoppingLists.RemoveProductsFromLists;

internal sealed class RemoveProductsFromListsCommandHandler(IApplicationDbContext context)
    : ICommandHandler<RemoveProductsFromListsCommand, int>
{
    public async Task<Result<int>> Handle(RemoveProductsFromListsCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            return Result.Failure<int>(ShopListErrors.UpdateError());
        }

        foreach (var list in request.RemoveRequests)
        {
            var shopList = await context.ShopLists
                .Include(sl => sl.ShopListProducts)
                .FirstOrDefaultAsync(sl => sl.Id == list.ShopListId, cancellationToken);

            if (shopList == null)
            {
                return Result.Failure<int>(ShopListErrors.NotFound(list.ShopListId));
            }

            var productsToRemove = shopList.ShopListProducts
                .Where(slp => list.ProductIds.Contains(slp.ProductId))
                .ToList();

            if (productsToRemove.Count > 0)
            {
                context.ShopListProducts.RemoveRange(productsToRemove);
            }
        }

        await context.SaveChangesAsync(cancellationToken);
        return Result.Success(1);
    }
}
