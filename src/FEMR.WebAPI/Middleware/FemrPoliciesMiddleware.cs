using Microsoft.Extensions.DependencyInjection;

namespace FEMR.WebAPI.Middleware
{
    public static class FemrPoliciesMiddleware
    {
        public static void AddFemrPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Users", policy => policy.RequireClaim("scopes", "users:read", "users:create"));
            });
        }
    }
}
