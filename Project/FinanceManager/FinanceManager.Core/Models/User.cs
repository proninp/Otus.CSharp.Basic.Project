using FinanceManager.Core.Models.Abstractions;

namespace FinanceManager.Core.Models;
public sealed class User : IdentityModel
{
    public long TelegramId { get; init; }

    public string? Username { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public ICollection<Account> Accounts { get; } = Array.Empty<Account>();

    public ICollection<Category> Categories { get; } = Array.Empty<Category>();

    public ICollection<Transaction> Transactions { get; } = Array.Empty<Transaction>();

    public User(long telegramId, string? username = null, string? firstname = null, string? lastname = null)
    {
        TelegramId = telegramId;
        Username = username;
        Firstname = firstname;
        Lastname = lastname;
    }
}