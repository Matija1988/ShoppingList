using App.Users.GetById;

namespace App.Users.GetAll;

public sealed record GetAllUsersResponse
{
    public List<UserResponse> Users { get; set; }
}
