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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseInfrastructure();

app.MapGet("nbp/exchangeRates", async ([FromServices] IQueryDispatcher dispatcher, string tableType, int topCount) =>
    {
        var exchangeRates = await dispatcher.QueryAsync(new GetExchangeRates(tableType, topCount));
        return exchangeRates.Count == 0 ? Results.NotFound() : Results.Ok(exchangeRates);
    })
    .WithName("GetExchangeRates")
    .WithOpenApi()
    .RequireAuthorization();

//NOTE: I didn't use there CQRS Command since commands should not return data
app.MapPost("user/sign-in", ([FromServices] IAuthenticationService authenticationService, SignIn signIn) =>
    {
        var result = authenticationService.SignIn(signIn.Username, signIn.Password);
        return result.IsSuccess ? Results.Ok(result.Value) : Results.Unauthorized();
    })
    .WithName("SignIn")
    .WithOpenApi();

app.Run();