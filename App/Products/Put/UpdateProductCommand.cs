namespace App.Products.Put;

public sealed record UpdateProductCommand(List<UpdateProduct> Products) : ICommand<int>;
