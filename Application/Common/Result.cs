namespace Application.Common;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public List<Error> Errors { get; } = [];
    public int StatusCode { get; }

    private Result(T value)
    {
        IsSuccess = true;
        Value = value;
        StatusCode = 200;
    }

    private Result(List<Error> errors, int statusCode)
    {
        IsSuccess = false;
        Errors = errors;
        StatusCode = statusCode;
    }

    public static Result<T> Success(T value) => new(value);

    public static Result<T> Failure(List<Error> errors, int statusCode = 400)
        => new(errors, statusCode);

    public static Result<T> Failure(string code, string message, int statusCode = 400)
        => new([new Error(code, message)], statusCode);
}
public class Result
{
    public bool IsSuccess { get; }
    public List<Error> Errors { get; } = [];
    public int StatusCode { get; }

    private Result()
    {
        IsSuccess = true;
        StatusCode = 200;
    }

    private Result(List<Error> errors, int statusCode)
    {
        IsSuccess = false;
        Errors = errors;
        StatusCode = statusCode;
    }

    public static Result Success() => new();

    public static Result Failure(List<Error> errors, int statusCode = 400)
        => new(errors, statusCode);

    public static Result Failure(string code, string message, int statusCode = 400)
        => new([new Error(code, message)], statusCode);
}