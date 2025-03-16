using FinanceManager.Bot.Models;

namespace FinanceManager.Bot.Services.Interfaces.Validators;
public interface ICategoryTitleValidator
{
    Task<(bool isValid, string newTitle)> ValidateAsync(BotUpdateContext context, string? categoryTitle);
}