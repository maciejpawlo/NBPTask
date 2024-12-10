using Microsoft.Extensions.DependencyInjection;
using NBPTask.Application.Services;

namespace NBPTask.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
}