using Microsoft.AspNetCore.Mvc;

namespace NBPTask.Shared.Results;
public interface IError
{
    string Code { get; init; }
    string? Message { get; init; }
}

public record NotFoundError(string Code, string? Message = null) : IError;
public record BadRequestError(string Code, string? Message = null) : IError;
public record UnauthorizedError(string Code, string? Message = null) : IError;