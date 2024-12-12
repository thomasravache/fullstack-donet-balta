using System.Reflection;
using System.Text;
using System.Web;

namespace Dima.Core.Common.Utils;

public class QueryStringBuilder
{
    private readonly Dictionary<string, string> _keyValuePairs;

    public QueryStringBuilder()
    {
        _keyValuePairs = [];
    }

    public static string BuildQueryParameters<T>(T obj) where T : class
    {
        var dictionary = GetKeyValuePairsFromObj(obj);

        return GetBuildedQuery(dictionary);
    }

    public static string BuildQueryParameters(IDictionary<string, string> iDictionary)
    {
        var dictionary = iDictionary.ToDictionary(x => x.Key, x => x.Value);

        return GetBuildedQuery(dictionary);
    }

    public static string BuildQueryParameter(string key, string value)
    {
        return GetBuildedQuery(new Dictionary<string, string>() { { key, value } });
    }

    public QueryStringBuilder AddQueryParameters<T>(T obj) where T : class
    {
        var dictionary = GetKeyValuePairsFromObj(obj);

        foreach (var item in dictionary)
            _keyValuePairs.Add(item.Key, item.Value);

        return this;
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

    public string BuildQueryAndCleanState()
    {
        CleanState();

        return BuildQuery();
    }

    public void CleanState()
    {
        _keyValuePairs.Clear();
    }

    public string BuildQuery()
    {
        return GetBuildedQuery(_keyValuePairs);
    }

    private static string GetBuildedQuery(Dictionary<string, string> dictionary)
    {
        if (dictionary.Count is 0)
            throw new InvalidOperationException($"A query string must have at least one parameter. Use the '{nameof(AddQueryParameter)}' method before, for example.");

        var stringBuilder = new StringBuilder();
        var firstItem = dictionary.First();

        stringBuilder.Append($"?{Encode(firstItem.Key)}={Encode(firstItem.Value)}");

        foreach (var item in dictionary.Where(x => x.Key != firstItem.Key))
        {
            stringBuilder.Append($"&{Encode(item.Key)}={Encode(item.Value)}");
        }

        return stringBuilder.ToString();
    }

    private static Dictionary<string, string> GetKeyValuePairsFromObj<T>(T obj) where T : class
    {
        var dictionary = new Dictionary<string, string>();

        foreach (PropertyInfo? prop in typeof(T).GetProperties())
        {
            var propName = prop.Name;
            var propValue = prop.GetValue(obj) ?? string.Empty;
            var propType = prop.PropertyType;

            if (propType == typeof(DateTime))
            {
                DateTime dt = (DateTime)propValue;
                propValue = dt.ToString("yyyy-MM-ddTHH:mm:ss");
            }

            dictionary.Add(propName, propValue.ToString() ?? string.Empty);
        }

        return dictionary;
    }

    private static string Encode(string text) => HttpUtility.UrlEncode(text);
}
