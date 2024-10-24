using Dima.Core.Common.Utils;
using Dima.Core.Extensions;

namespace Dima.Core.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var urlBase = "api/v1/transaction";

        var query = new QueryStringBuilder()
            .AddQueryParameter("teste", "valor1")
            .AddQueryParameter("teste2", "valor2")
            .BuildQuery();

        Console.WriteLine(urlBase);
    }
}