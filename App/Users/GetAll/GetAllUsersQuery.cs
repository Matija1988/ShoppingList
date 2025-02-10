using App.Abstractions.Messaging;

namespace App.Users.GetAll;

public sealed record GetAllUsersQuery() : IQuery<GetAllUsersResponse>;