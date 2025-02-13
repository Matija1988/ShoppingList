namespace App.ShoppingLists.RemoveProductsFromList;

public sealed record RemoveProductsFromListDTO(int ShopListId, List<int> ProductIds);
