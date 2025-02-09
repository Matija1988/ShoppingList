namespace Infrastructure.Authorization;

internal sealed class PermissionProvider
{
    public async Task<HashSet<string>> GetForUserIdAsync(Guid userId)
    {
        //TODO: Logit to fetch permissions
        HashSet<string> permissionsSet = new HashSet<string>();

        if (userId == Guid.Parse("d4ef1b5b-d953-4eed-8045-f786ed22502d")) // Example userId
        {
            permissionsSet.Add("CanViewDashboard");
            permissionsSet.Add("CanEditProfile");
        }

        return await Task.FromResult(permissionsSet);
    } 
}
