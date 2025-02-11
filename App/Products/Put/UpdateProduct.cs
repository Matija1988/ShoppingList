namespace App.Products.Put;

public sealed record UpdateProduct(int Id, string Name, decimal UnitPrice, bool IsActive);