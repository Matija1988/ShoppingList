namespace Domain.Users;

public static class UserErrors
{
    public static Error NotFound(Guid userId) => Error.NotFound
        ("Users.NotFound",
        $"User with the Id = '{userId}' was not found");
    public static Error Unauthorized() => Error.Failure(
        "Users.Unauthorized",
        "You are not authorized to perform this action.");

    public static readonly Error NotFoundByEmail = Error.NotFound(
        "Users.NotFoundByEmail",
        "The user with the specified email was not found!");

    public static readonly Error NotFoundByEmailOrUsername = Error.NotFound(
      "Users.NotFoundByEmailOrUsername",
      "The user with the specified email and/or username was not found!");

    public static readonly Error InvalidPasword = Error.NotFound(
  "Users.InvalidPassword",
  "Check your input and try again!");

    public static readonly Error EmailNotUnique = Error.Conflict(
        "Users.EmailNotUnique",
        "The provided email is not unique");

    public static readonly Error UsernameNotUnique = Error.Conflict(
        "Users.UsernameNotUnique",
        "The provided Username is already in use!");
}
