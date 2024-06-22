namespace Dima.Core.Responses;

public class Response<TData>
{
    public Response(bool isSuccess, TData? data, string? message = null)
    {
        Data = data;
        IsSuccess = isSuccess;

        if (message is not null)
            Messages.Add(message);
    }

    public Response(bool isSuccess, TData? data, IList<string>? messages = null)
    {
        Data = data;
        IsSuccess = isSuccess;

        if (messages is not null)
            Messages.AddRange(messages);
    }

    public TData? Data { get; set; }
    public List<string> Messages { get; set; } = [];
    public string? PrincipalMessage => Messages.FirstOrDefault();
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public static Response<TData> Failure(string message)
        => new(false, default, message);
    public static Response<TData> Failure(IList<string> messages)
        => new(false, default, messages);

    public static Response<TData> Success(TData? data, string? message = null)
        => new(true, data, message);
    public static Response<TData> Success(TData? data, IList<string>? messages)
        => new(true, data, messages);
}