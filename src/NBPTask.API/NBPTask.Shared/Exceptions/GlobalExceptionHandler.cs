using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NBPTask.Shared.Exceptions;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, 
    IWebHostEnvironment webHostEnvironment) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
    
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var request = httpContext.Request;
        _logger.LogError(exception, $"An exception occured while handling request {request.Method}: {request.Path}");

        var response = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Unhandled exception",
            Detail = exception.GetType().Name,
            Extensions =
            {
                ["message"] = exception.Message
            }
        };
        
        if (_webHostEnvironment.IsDevelopment())
            response.Extensions["stackTrace"] = exception.StackTrace;

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        return true;
    }
}