using App.Abstractions.Messaging;

namespace App.Users.Register;

public sealed record RegisterUserCommand(string Email, string Username, string Password) : ICommand<Guid>;