namespace App.ShoppingLists.Post;

public sealed record PostShopListCommand : ICommand<List<int>>
{
    public List<PostShopList> ShopLists { get; set; }

    public PostShopListCommand(List<PostShopList> shopLists) => this.ShopLists = shopLists;
}
