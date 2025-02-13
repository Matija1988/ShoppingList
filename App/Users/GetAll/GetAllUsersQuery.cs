using App.Users.GetById;

namespace App.Users.GetAll;

public sealed record GetAllUsersQuery() : IQuery<List<UserResponse>>;