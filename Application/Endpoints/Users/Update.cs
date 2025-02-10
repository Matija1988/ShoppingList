
using App.Users.Update;

namespace Web.Api.Endpoints.Users;

internal sealed class Update : IEndpoint
{
    public sealed record Request(string Email, string Username, string Password, bool IsActive);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("users/{userId}", async (Guid userId, Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new UpdateUserCommand(
                userId,
                request.Email,
                request.Username,
                request.Password,
                request.IsActive
                );

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .HasPermission(Permissions.UserAccess)
        .HasPermission(Permissions.UserEditing)
        .WithTags(Tags.Users);
    }
}
