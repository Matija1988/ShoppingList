using App.Users.Login;

namespace Web.Api.Endpoints.Users;

internal sealed class Login : IEndpoint
{
    public sealed record Request(string? Email, string? Username, string Password);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/login", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new LoginUserCommand(request.Email, request.Username, request.Password);

            Result<string> results = await sender.Send(command, cancellationToken);

            return results.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }

}
