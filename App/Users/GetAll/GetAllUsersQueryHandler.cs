using App.Users.GetById;

namespace App.Users.GetAll;

internal sealed class GetAllUsersQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetAllUsersQuery, List<UserResponse>>
{
    public async Task<Result<List<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
         var response =  await context.Users.Select(
            u => new UserResponse
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        if(response is null)
        {
            return Result.Failure<List<UserResponse>>(UserErrors.NoUsersInDb);
        }

        return response;
    }
}
