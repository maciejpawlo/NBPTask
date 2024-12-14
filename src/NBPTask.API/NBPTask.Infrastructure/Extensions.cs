using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NBPTask.Application.Services;
using NBPTask.Domain.Clients.NBP;
using NBPTask.Domain.Repositories;
using NBPTask.Infrastructure.Auth;
using NBPTask.Infrastructure.Clients;
using NBPTask.Infrastructure.Repositories;
using NBPTask.Shared.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NBPTask.Shared;

namespace NBPTask.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        services.AddHttpClient<INbpApiClient, NbpApiClient>(client =>
        {
            var apiUrl = configuration["Nbp:ApiUrl"]!;
            client.BaseAddress = new Uri(apiUrl);
        });
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJwtManager, JwtManager>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!))
                };
            });
        services.AddAuthorization();
        services.AddQueries();
        services.AddCors(p => p.AddPolicy("mycors", builder =>
        {
            builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
        }));

        return services;
    }

    public static void UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseCors("mycors");
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseExceptionHandler();
    }
}