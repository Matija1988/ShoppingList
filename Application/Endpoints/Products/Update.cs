using App.Products.Put;

namespace Web.Api.Endpoints.Products;

internal sealed class Update : IEndpoint
{
    public sealed record Request(List<UpdateProduct> Products);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("products", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new UpdateProductCommand(
                request.Products
                );

            Result<int> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Products)
        .RequireAuthorization();
    }
}
