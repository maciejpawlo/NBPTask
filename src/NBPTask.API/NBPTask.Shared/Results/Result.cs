using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NBPTask.Shared.Results;

public class Result<T>
{
    private Result(T value)
    {
        IsSuccess = true;
        Value = value;
        Error = default;
    }
    
    private Result(IError error)
    {
        IsSuccess = false;
        Value = default;
        Error = error;
    }

    [MemberNotNullWhen(true, nameof(Value))]
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess { get; }
    public T? Value { get; }
    public IError? Error { get; }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Fail(IError error) => new(error);

    public static implicit operator Result<T>(T value) => Success(value);

    public IResult ToApiResult() => IsSuccess
        ? Microsoft.AspNetCore.Http.Results.Ok(Value)
        : Microsoft.AspNetCore.Http.Results.Problem(CreateProblemDetails(Error));
    
    private static ProblemDetails CreateProblemDetails(IError error)
    {
        return new ProblemDetails
        {
            Status = error.AsStatusCode(),
            Detail = error.Message,
            Title = GetTitle(error),
            Extensions = 
            {
                ["errorCode"] = error.Code
            }
        };
    }

    private static string GetTitle(IError error)
    {
        var errorName = error.GetType().Name.Replace("error", string.Empty, StringComparison.InvariantCultureIgnoreCase);
        var splitCamelCaseRegex = new Regex(@"(?<!^)(?=[A-Z])");
        return string.Join(" ", splitCamelCaseRegex.Split(errorName));
    }
}

