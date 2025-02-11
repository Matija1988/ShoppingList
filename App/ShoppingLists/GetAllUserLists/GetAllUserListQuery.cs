using Domain.ShopList;

namespace App.ShoppingLists.GetAllUserLists;

public sealed record GetAllUserListQuery(Guid userId) : IQuery<List<ShopListReadResponse>>; 