using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NBPTask.Domain.Clients.NBP;
using NBPTask.Infrastructure.Clients;
using NBPTask.Shared.Exceptions;

namespace NBPTask.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddHttpClient<INbpApiClient, NbpApiClient>((serviceProvider, client) =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var apiUrl = configuration.GetValue<string>("Nbp:ApiUrl") 
                         ?? throw new InvalidOperationException("No NBP API url defined in configuration");
            client.BaseAddress = new Uri(apiUrl);
        });
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        return services;
    }

    public static void UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseExceptionHandler();
    }
}