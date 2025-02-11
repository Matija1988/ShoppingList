namespace App.Users.GetById;

internal sealed class GetByIdHandler(IApplicationDbContext context)
    : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        UserResponse? user = await context.Users
            .Where(u => u.Id == query.UserId)
            .Select(u => new UserResponse
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
            })
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);

        return user is not null
               ? Result.Success(user)
               : Result.Failure<UserResponse>(UserErrors.NotFound(query.UserId));
    }
}
