using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NBPTask.Application;
using NBPTask.Application.Commands;
using NBPTask.Application.Queries;
using NBPTask.Application.Services;
using NBPTask.Infrastructure;
using NBPTask.Shared.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

app.UseInfrastructure();

app.MapGet("nbp/exchange-rates", async ([FromServices] IQueryDispatcher dispatcher, string tableType, int topCount) =>
    {
        var exchangeRatesResult = await dispatcher.QueryAsync(new GetExchangeRates(tableType, topCount));
        return exchangeRatesResult.ToApiResult();
    })
    .WithName("GetExchangeRates")
    .WithOpenApi()
    .RequireAuthorization();

//NOTE: I didn't use there CQRS Command since commands should not return data
app.MapPost("user/sign-in", ([FromServices] IAuthenticationService authenticationService, SignIn signIn) =>
    {
        var result = authenticationService.SignIn(signIn.Username, signIn.Password);
        return result.ToApiResult();
    })
    .WithName("SignIn")
    .WithOpenApi();

app.Run();