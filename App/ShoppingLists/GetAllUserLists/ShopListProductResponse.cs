namespace App.ShoppingLists.GetAllUserLists;

public sealed record ShopListProductResponse
{
    public ProductResponse Product { get; set; }
    public int ProductQuantity { get; set; }
    public decimal TotalValue { get; set; }
}