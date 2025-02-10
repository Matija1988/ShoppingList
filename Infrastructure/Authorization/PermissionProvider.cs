namespace Infrastructure.Authorization;

internal sealed class PermissionProvider
{
    public async Task<HashSet<string>> GetForUserIdAsync(Guid? userId)
    {
        HashSet<string> permissionsSet = new HashSet<string>();

        if (userId == Guid.Parse("ABDF2D1D-B705-4647-99FE-E7C298AC74BB"))
        {
            permissionsSet.Add("users:access");
            permissionsSet.Add("users:allowEdit");
        }

        if(userId == null || userId == Guid.Empty)
        {
            permissionsSet.Add("users:access");
        }

        return await Task.FromResult(permissionsSet);
    } 
}
