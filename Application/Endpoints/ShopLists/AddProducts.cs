using App.ShoppingLists.AddProductsToList;

namespace Web.Api.Endpoints.ShopLists;

internal sealed class AddProducts : IEndpoint
{
    public sealed record Request(AddProductsToList productsToList);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("addToList", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new AddProductsToListCommand(request.productsToList);

            Result<int> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);            
        })
        .WithTags(Tags.ShopLists);
    }
}
