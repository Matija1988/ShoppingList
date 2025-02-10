using App.Products.GetAll;
using App.Products.GetById;

namespace Web.Api.Endpoints.Products;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products/{productId}", async (int productId, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetProductByIdQuery(productId);

            Result<ProductResponse> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Products)
        .RequireAuthorization();
    }
}
