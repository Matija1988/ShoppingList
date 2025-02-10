using App.Abstractions.Messaging;

namespace App.Users.Update;

public sealed record UpdateUserCommand(Guid Id, string Email, string Username, string Password, bool IsActive) 
    : ICommand<Guid>;
