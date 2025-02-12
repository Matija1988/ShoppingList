
using Domain.ShopList;

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



        throw new NotImplementedException();
    }
}
