using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Authorization;

internal sealed class PermissionAuthorizationHandler(IServiceProvider serviceScopeFactory)
    : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {

        //TODO: Reject unauthenticaed users here
        if(context.User?.Identity?.IsAuthenticated != true)
        {
            context.Fail();
            return;
        }

        using IServiceScope scope = serviceScopeFactory.CreateScope();
        PermissionProvider permissionProvider = scope.ServiceProvider.GetRequiredService<PermissionProvider>();

        Guid userId = context.User.GetUserId();

        HashSet<string> permissions = await permissionProvider.GetForUserIdAsync(userId);

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }

    }
}
