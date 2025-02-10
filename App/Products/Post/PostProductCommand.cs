namespace App.Products.Post;

public sealed record PostProductCommand(List<PostProduct> Products) : ICommand<int>;
