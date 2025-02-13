namespace App.ShoppingLists.Post;

public sealed record PostShopList(string Name, Guid Owner, List<PostShopListProduct> Products);