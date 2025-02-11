namespace App.Products.Put;

internal sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProduct>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(50).WithMessage("Maximum allowed number of characters = 50");
        RuleFor(c => c.UnitPrice).NotEmpty()
            .InclusiveBetween(0.01m, 10000.00m)
            .WithMessage("Unit price must be between 0.01 and 10000.00!");
    }
}
