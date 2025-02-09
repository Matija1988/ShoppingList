using App.Abstractions.Messaging;

namespace App.Users.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;