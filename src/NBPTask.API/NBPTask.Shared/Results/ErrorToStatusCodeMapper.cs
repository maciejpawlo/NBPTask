using Microsoft.AspNetCore.Http;

namespace NBPTask.Shared.Results;

public static class ErrorToStatusCodeMapper
{
    public static int AsStatusCode(this IError error) => error switch
    {
        UnauthorizedError => StatusCodes.Status401Unauthorized,
        NotFoundError => StatusCodes.Status404NotFound,
        BadRequestError => StatusCodes.Status400BadRequest,
        _ => StatusCodes.Status500InternalServerError
    };
}