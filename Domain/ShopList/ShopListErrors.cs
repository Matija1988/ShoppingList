namespace Domain.ShopList;

public static class ShopListErrors
{
    public static Error NotFound(int entityId) => Error.NotFound(
        "ShopList.NotFound",
        $"The list with the Id = '{entityId}' was not found!");

    public static Error NoUserList(Guid UserId) => Error.NotFound(
        "ShopList.NotFound",
        $"The user with the Id = '{UserId}' has no lists to load!");

    public static Error PostError() => Error.Conflict(
        "ShopList.PostIssue",
        "Unable to insert shop list into database!");

    public static Error UpdateError() => Error.Failure(
        "ShopList.PutIssue",
        "Problem updating shopping list!");

}
