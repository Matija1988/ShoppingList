using App.ShoppingLists.GetAllUserLists;
using Domain.ShopList;

namespace Web.Api.Endpoints.ShopLists;

public class GetUserLists : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("shopLists/{userId}", async (Guid userId, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetAllUserListQuery(userId);

            Result<List<ShopListReadResponse>> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.ShopLists);
    }
}
