namespace App.Products.GetAll;

public sealed record ProductResponse
{
    public int Id { get; init; }

    public decimal UnitPrice { get; init; }

    public string Name { get; init; }

    public DateTime DateUpdated { get; init; }
}