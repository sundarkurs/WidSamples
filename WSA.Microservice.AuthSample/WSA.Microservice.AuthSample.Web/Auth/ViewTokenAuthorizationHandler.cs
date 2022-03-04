using Microsoft.AspNetCore.Authorization;

namespace WSA.Microservice.AuthSample.Web.Auth
{
    public class ViewTokenAuthorizationHandler : AuthorizationHandler<ViewTokenRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ViewTokenRequirement requirement)
        {
            if (requirement.AllowViewToken)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
