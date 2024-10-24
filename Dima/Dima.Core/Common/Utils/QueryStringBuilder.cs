using System.Text;

namespace Dima.Core.Common.Utils;

public class QueryStringBuilder
{
    private readonly Dictionary<string, string> _keyValuePairs;

    public QueryStringBuilder()
    {
        _keyValuePairs = [];
    }

    public QueryStringBuilder AddQueryParameters(IDictionary<string, string> dictionary)
    {
        foreach (var item in dictionary)
            _keyValuePairs.Add(item.Key, item.Value);

        return this;
    }

    public QueryStringBuilder AddQueryParameter(string key, string value)
    {
        _keyValuePairs.Add(key, value);

        return this;
    }

    public string BuildQuery()
    {
        var builder = new StringBuilder();
        bool isFirstItem = true;

        foreach (var item in _keyValuePairs)
        {
            if (isFirstItem)
            {
                builder.Append($"?{item.Key}={item.Value}");
                isFirstItem = false;
                continue;
            }

            builder.Append($"&{item.Key}={item.Value}");
        }

        return builder.ToString();
    }
}