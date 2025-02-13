namespace App.ShoppingLists.GetAllUserLists;

public sealed record ShopListReadResponse
{
    public int ShopListId { get; init; }
    public string ShopListName { get; init; }
    public DateTime DateUpdated { get; init; }
    public bool IsActive { get; set; }
    public List<ShopListProductResponse> Products { get; set; }
    public decimal ShopListTotalValue { get; init; }

}
