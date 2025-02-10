using App.Abstractions.Authentication;
using App.Abstractions.Data;
using App.Abstractions.Messaging;
using App.Users.GetById;

namespace App.Users.GetAll;

internal sealed class GetAllUsersQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetAllUsersQuery, GetAllUsersResponse>
{
    public async Task<Result<GetAllUsersResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        GetAllUsersResponse response = new GetAllUsersResponse();
          response.Users =  await context.Users.Select(
            u => new UserResponse
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
            })
            .ToListAsync(cancellationToken);

        if(response is null)
        {
            return Result.Failure<GetAllUsersResponse>(UserErrors.NoUsersInDb);
        }

        return response;
    }
}
