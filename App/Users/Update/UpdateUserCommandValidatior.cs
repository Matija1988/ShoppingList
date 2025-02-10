namespace App.Users.Update;

internal sealed class UpdateUserCommandValidatior : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidatior()
    {
        RuleFor(c => c.Username).NotEmpty();
        RuleFor(c => c.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.Password).NotEmpty().MinimumLength(8);
        RuleFor(c => c.IsActive).NotNull();
    }
}
