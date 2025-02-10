using App.Abstractions.Messaging;
using App.Products.GetAll;

namespace App.Products.GetById;

public sealed record GetProductByIdQuery(int Id) : IQuery<ProductResponse>;
