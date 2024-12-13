using FinanceManager.Bot.Enums;

namespace FinanceManager.Bot.Models;
public class UserSession
{
    public Guid Id { get; init; }
    
    public long TelegramId { get; init; }
    
    public string? UserName { get; set; }

    public UserState UserState { get; set; }
}
