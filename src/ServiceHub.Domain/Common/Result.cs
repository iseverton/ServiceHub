namespace ServiceHub.Domain.Common;

public sealed class Result<T>
{
    public T? Data { get; set; }
    public bool IsSuccess { get; set; }
    public List<string>? Errors { get; set; }

    private Result(T? data, bool isSuccess, List<string> errors)
    {
        Data = data;
        IsSuccess = isSuccess;
        Errors = errors ?? new List<string>();
    }

    public static Result<T> Success(T? data) => new(data, true, new List<string>());
    public static Result<T> Success() => new(default, true, new List<string>());
    public static Result<T> Fail(List<string> error) => new(default, false, error);
    public static Result<T> Fail(string error) => new(default, false, new List<string> { error });

}

public sealed class NoContent
{  
}
