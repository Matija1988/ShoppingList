using App.Users.GetAll;

namespace Web.Api.Endpoints.Users;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users", async (ISender sender, CancellationToken cancelationToken) =>
        {
            var query = new GetAllUsersQuery();

            Result<GetAllUsersResponse> result = await sender.Send(query, cancelationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .HasPermission(Permissions.UserAccess)
        .WithTags(Tags.Users);
    }
}
