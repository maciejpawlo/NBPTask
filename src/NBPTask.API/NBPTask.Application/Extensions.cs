using Microsoft.Extensions.DependencyInjection;
using NBPTask.Shared;

namespace NBPTask.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddQueries();
        return services;
    }
}