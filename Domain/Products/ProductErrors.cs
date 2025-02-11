namespace Domain.Products;

public static class ProductErrors
{
    public static Error NotFound(int productId) => Error.NotFound(
        "Products.NotFound",
        $"The products with the Id = '{productId}' was not found!");

    public static Error ProductsInDb() => Error.Conflict(
        "Products.PostIssue",
        "Products already in database!");

    public static Error UpdateError() => Error.Failure(
        "Products.PutIssue",
        "Problem updating products!");

    public static Error BatchRequestEmpty() => Error.Failure(
        "Products.PutIssue",
        "Batch request didn't contain any valid products!");

    public static readonly Error NotFoundByName = Error.NotFound(
        "Products.NotFoundByName",
        "The product with the specified name was not found!");

    public static readonly Error ProductsNotFound = Error.NotFound(
        "Products.NotFound",
        "No products found in database!");
}
