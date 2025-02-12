namespace App.ShoppingLists.AddProductsToList;

public sealed record AddProductsToList(int shopListId, List<AddProductsToListDTO> Products);
