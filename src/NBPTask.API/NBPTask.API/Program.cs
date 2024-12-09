using Microsoft.AspNetCore.Mvc;
using NBPTask.Application;
using NBPTask.Application.Queries;
using NBPTask.Infrastructure;
using NBPTask.Shared.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseInfrastructure();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("nbp/exchangeRates", async ([FromServices]IQueryDispatcher dispatcher, string tableType, int topCount) => 
{
    var exchangeRates = await dispatcher.QueryAsync(new GetExchangeRatesQuery(tableType, topCount));
    return exchangeRates.Count == 0 ? Results.NotFound() : Results.Ok(exchangeRates);
})
.WithName("GetExchangeRates")
.WithOpenApi();

app.Run();