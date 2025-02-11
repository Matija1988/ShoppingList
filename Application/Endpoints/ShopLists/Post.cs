using App.ShoppingLists.Post;

namespace Web.Api.Endpoints.ShopLists;

internal sealed class Post : IEndpoint
{
    public sealed record Request(PostShopList shopList);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("shopList", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new PostShopListCommand(request.shopList);

            Result<int> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.ShopLists);
    }
}
