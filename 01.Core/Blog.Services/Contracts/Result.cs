namespace Services.Contracts;

public abstract class Result
{
    protected Result(bool isSuccess, Error? error)
    {
        if (isSuccess && error != null || !isSuccess && error == null)
            throw new InvalidOperationException();
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; private set; }
    public Error? Error { get; private set; }
}

public class Result<T> : Result
{
    private Result(T value) : base(true, null) => Value = value;
    private Result(Error error) : base(false, error) => Value = default;
    public T? Value { get; }
    public static implicit operator Result<T>(T value) => new(value);
    public static implicit operator Result<T>(Error error) => new(error);
}

public class Error
{
    public Error(string code, ErrorType type, string message)
    {
        Code = code;
        Type = type;
        Message = message;
    }

    public string Code { get; private set; }
    public ErrorType Type { get; private set; }
    public string Message { get; private set; }
}

public enum ErrorType
{
    NotFound,
    InvalidInput,
    Forbidden,
    Unauthorized,
    Validation
}