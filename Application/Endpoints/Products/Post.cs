using App.Products.Post;

namespace Web.Api.Endpoints.Products;

internal sealed class Post : IEndpoint
{
    public sealed record Request(List<PostProduct> Products);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("products", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new PostProductCommand(
                request.Products
                );

            Result<int> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Products)
        .RequireAuthorization();
    }
}
