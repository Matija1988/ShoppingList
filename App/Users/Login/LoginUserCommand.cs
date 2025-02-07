using App.Abstractions.Messaging;

namespace App.Users.Login;

public sealed record LoginUserCommand(string? Email, string? Username, string Password) : ICommand<string>;
