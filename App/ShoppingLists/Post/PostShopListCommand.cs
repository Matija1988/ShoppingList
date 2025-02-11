namespace App.ShoppingLists.Post;

public sealed record PostShopListCommand(PostShopList ShopList) : ICommand<int>;
