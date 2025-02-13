using App.ShoppingLists.RemoveProductsFromList;
using App.ShoppingLists.RemoveProductsFromLists;

namespace Web.Api.Endpoints.ShopLists;

public class RemoveProducts : IEndpoint
{
    public sealed record Request(List<RemoveProductsFromListDTO> request);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("removeProductsFromList", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new RemoveProductsFromListsCommand(request.request);

            Result<int> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.ShopLists);
    }
}
