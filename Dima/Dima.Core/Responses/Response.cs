using System.Text.Json.Serialization;

namespace Dima.Core.Responses;

public class Response<TData>
{
    private const int DefaultStatusCode = 200;

    private readonly int _code = DefaultStatusCode;

    [JsonConstructor]
    public Response() {  }

    public Response(TData? data, int code = DefaultStatusCode, string? message = null)
    {
        Data = data;
        Message = message;
        _code = code;
    }

    public TData? Data { get; set; }
    public string? Message { get; set; } = string.Empty;

    public bool IsSuccess => _code is >= 200 and <= 299;
}