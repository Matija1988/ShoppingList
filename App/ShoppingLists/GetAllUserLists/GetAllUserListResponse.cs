using Domain.ShopList;

namespace App.ShoppingLists.GetAllUserLists;

public sealed record GetAllUserListResponse
{
    public List<ShopList> ShopLists { get; set; }
}
