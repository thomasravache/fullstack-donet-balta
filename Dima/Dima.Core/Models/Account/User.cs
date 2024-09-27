namespace Dima.Core.Models.Account;

public class User
{
    // v1/identity/manage/info - retem infos do usuário
    public string Email { get; set; } = string.Empty;
    public Dictionary<string, string> Type { get; set; } = [];
}