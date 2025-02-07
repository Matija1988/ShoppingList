using App.Abstractions.Messaging;
using App.DTO;

namespace App.Users.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserReadResponse>;