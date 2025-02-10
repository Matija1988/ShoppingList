using App.Abstractions.Authentication;

namespace App.Users.Login;

internal sealed class LoginUserCommandHandler(
    IApplicationDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider) : ICommandHandler<LoginUserCommand, string>
{
    public async Task<Result<string>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email == command.Email || u.Username == command.Username && u.IsActive == true, cancellationToken);

        if (user is null) 
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmailOrUsername);
        }

        bool verified = passwordHasher.Verify(command.Password, user.Password);

        if (!verified)
        {
            return Result.Failure<string>(UserErrors.InvalidPasword);
        }

        string token = tokenProvider.Create(user);

        return token;
    }
}
