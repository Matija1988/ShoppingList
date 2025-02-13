using App.ShoppingLists.Post;

namespace Web.Api.Endpoints.ShopLists;

internal sealed class Post : IEndpoint
{
    public sealed record Request(List<PostShopList> shopList);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("shopLists", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new PostShopListCommand(request.shopList);

            Result<List<int>> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.ShopLists);
    }
}
