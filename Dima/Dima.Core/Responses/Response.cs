using System.Text.Json.Serialization;

namespace Dima.Core.Responses;

public class Response<TData>
{
    private readonly int _code = Configuration.DefaultStatusCode;

    [JsonConstructor]
    public Response() {  }

    public Response(TData? data, int code = Configuration.DefaultStatusCode, string? message = null)
    {
        Data = data;
        _code = code;

        if (message is not null)
            Messages.Add(message);
    }

    public Response(TData? data, int code, IList<string>? messages = null)
    {
        Data = data;
        _code = code;

        if (messages is not null)
            Messages.AddRange(messages);
    }

    public TData? Data { get; set; }
    public List<string> Messages { get; set; } = [];
    public string? PrincipalMessage => Messages.FirstOrDefault();

    public bool IsSuccess => _code is >= 200 and <= 299;
}