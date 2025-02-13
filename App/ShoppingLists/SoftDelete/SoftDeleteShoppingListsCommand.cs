namespace App.ShoppingLists.SoftDelete;

public sealed record SoftDeleteShoppingListsCommand(List<int> ShopListIds) : ICommand<int>;
