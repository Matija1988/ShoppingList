namespace App.Users.Register;

internal sealed class CustomEmailValidator : AbstractValidator<string>
{
    public CustomEmailValidator()
    {
        RuleFor(email => email)
            .NotEmpty().WithMessage("Email is required")
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("Invalid email format");
    }
}
