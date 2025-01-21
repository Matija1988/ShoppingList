namespace Infrastructure.Authorization;

internal sealed class PermissionProvider
{
    public async Task<HashSet<string>> GetForUserIdAsync(Guid userId)
    {
        //TODO: Logit to fetch permissions

        HashSet<string> premissionsSet = [];

        return await Task.FromResult(premissionsSet);
    } 
}
