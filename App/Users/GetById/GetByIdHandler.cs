using App.Abstractions.Authentication;
using App.Abstractions.Data;
using App.Abstractions.Messaging;
using App.DTO;

namespace App.Users.GetById;

internal sealed class GetByIdHandler(IApplicationDbContext context, IUserContext userContext)
    : IQueryHandler<GetUserByIdQuery, UserReadResponse>
{
    public async Task<Result<UserReadResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        if (query.UserId != userContext.UserId)
        {
            return Result.Failure<UserReadResponse>(UserErrors.Unauthorized());
        }

        UserReadResponse? user = await context.Users
            .Where(u => u.Id == query.UserId)
            .Select(u => new UserReadResponse
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                DateCreated = u.DateCreated,
                DateUpdated = u.DateUpdated,
                IsActive = u.IsActive,
                Password = u.Password,
            })
            .SingleOrDefaultAsync(cancellationToken);

        return user;
    }
}
