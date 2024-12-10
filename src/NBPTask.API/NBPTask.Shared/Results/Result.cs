using System.Diagnostics.CodeAnalysis;

namespace NBPTask.Shared.Results;

public class Result<T>
{
    private Result(T value)
    {
        IsSuccess = true;
        Value = value;
        Error = default;
    }
    
    private Result(Error error)
    {
        IsSuccess = false;
        Value = default;
        Error = error;
    }

    [MemberNotNullWhen(true, nameof(Value))]
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess { get; }
    public T? Value { get; }
    public Error? Error { get; }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Fail(Error error) => new(error);
    
    public static implicit operator Result<T>(T value) => Success(value);
}

public record Error(string Code, string? Message = null);