using App.Abstractions.Authentication;
using App.Abstractions.Data;
using App.Abstractions.Messaging;

namespace App.Users.Register;

internal sealed class RegisterUserCommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher) : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (await context.Users.AnyAsync(u => u.Email == command.Email, cancellationToken)) 
        {
            return Result.Failure<Guid>(UserErrors.EmailNotUnique);
        }

        if (await context.Users.AnyAsync(u => u.Username == command.Username, cancellationToken))
        {
            return Result.Failure<Guid>(UserErrors.UsernameNotUnique);
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = command.Email,
            Username = command.Username,
            Password = passwordHasher.Hash(command.Password),
            IsActive = true,
            DateCreated = DateTime.Now,
            DateUpdated = DateTime.Now,
        };

        user.Raise(new UserRegisteredDomainEvent(user.Id));

        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user.Id;
    }
}
