using FinanceManager.Core.Models.Abstractions;

namespace FinanceManager.Core.Models;
public class User : BaseModel
{
    public long TelegramId { get; init; }

    public string? Name { get; set; }

    public ICollection<Account> Accounts { get; } = Array.Empty<Account>();

    public ICollection<Category> Categories { get; } = Array.Empty<Category>();

    public ICollection<Transaction> Transactions { get; } = Array.Empty<Transaction>();

    public User(long telegramId, string? name = null)
    {
        TelegramId = telegramId;
        Name = name;
    }
}