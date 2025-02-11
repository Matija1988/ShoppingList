using App.Abstractions.Authentication;

namespace App.Users.Update;

internal sealed class UpdateUserCommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher)
    : ICommandHandler<UpdateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        if (await context.Users.AnyAsync(u => u.Email == command.Email, cancellationToken))
        {
            return Result.Failure<Guid>(UserErrors.EmailNotUnique);
        }

        if (await context.Users.AnyAsync(u => u.Username == command.Username, cancellationToken))
        {
            return Result.Failure<Guid>(UserErrors.UsernameNotUnique);
        }

        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == command.Id);

        if (user is null) 
        {
            return Result.Failure<Guid>(UserErrors.NotFound(command.Id));
        }

        user.Email = command.Email;
        if (user.Password != command.Password)
        {
            user.Password = passwordHasher.Hash(command.Password);
        }
        user.Username = command.Username;
        user.IsActive = command.IsActive;

        context.Users.Update(user);
        await context.SaveChangesAsync(cancellationToken);

        return command.Id;
    }
}
