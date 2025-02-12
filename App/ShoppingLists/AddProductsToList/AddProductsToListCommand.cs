namespace App.ShoppingLists.AddProductsToList;

public sealed record AddProductsToListCommand(AddProductsToList AddProductsToList) : ICommand<int>;
