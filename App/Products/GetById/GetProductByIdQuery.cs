namespace App.Products.GetById;

public sealed record GetProductByIdQuery(int Id) : IQuery<ProductResponse>;
