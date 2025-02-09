namespace App.Users.GetById;

public sealed record UserResponse
{
    public Guid Id { get; init; }
    public string Email { get; init; }
    public string Username { get; init; }

}
