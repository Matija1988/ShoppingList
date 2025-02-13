using App.ShoppingLists.RemoveProductsFromList;

namespace App.ShoppingLists.RemoveProductsFromLists;

public sealed record RemoveProductsFromListsCommand : ICommand<int>
{
    public List<RemoveProductsFromListDTO> RemoveRequests { get; set; }

    public RemoveProductsFromListsCommand(List<RemoveProductsFromListDTO> removeRequests)
    {
        this.RemoveRequests = removeRequests;
    }
}
