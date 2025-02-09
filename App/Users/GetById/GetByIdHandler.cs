using App.Abstractions.Authentication;
using App.Abstractions.Data;
using App.Abstractions.Messaging;

namespace App.Users.GetById;

internal sealed class GetByIdHandler(IApplicationDbContext context, IUserContext userContext)
    : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        if (query.UserId != userContext.UserId)
        {
            return Result.Failure<UserResponse>(UserErrors.Unauthorized());
        }

        UserResponse? user = await context.Users
            .Where(u => u.Id == query.UserId)
            .Select(u => new UserResponse
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
            })
            .SingleOrDefaultAsync(cancellationToken);

        return user is not null
    ? Result.Success(user)
    : Result.Failure<UserResponse>(UserErrors.NotFound(query.UserId));
    }
}
