namespace App.Products.GetAll;

public sealed record GetAllProductsQuery() : IQuery<List<ProductResponse>>;