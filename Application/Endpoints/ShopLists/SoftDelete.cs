using App.ShoppingLists.SoftDelete;

namespace Web.Api.Endpoints.ShopLists;

internal sealed class SoftDelete : IEndpoint
{
    public sealed record Request(List<int> ShopListIds);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("disableShopLists", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new SoftDeleteShoppingListsCommand(request.ShopListIds);

            Result<int> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.ShopLists);
    }
}
