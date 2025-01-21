using App.DTO;

namespace App.Services.Users;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserReadResponse>;