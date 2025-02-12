using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Validators;

namespace FinanceManager.Bot.Services.StateHandlers.Validators;
public class CategoryTitleValidator : ICategoryTitleValidator
{
    private readonly IMessageManager _messageManager;
    private readonly ICategoryManager _categoryManager;

    public CategoryTitleValidator(IMessageManager messageManager, ICategoryManager categoryManager)
    {
        _messageManager = messageManager;
        _categoryManager = categoryManager;
    }

    public async Task<bool> Validate(BotUpdateContext context, string? categoryTitle)
    {
        if (string.IsNullOrEmpty(categoryTitle))
        {
            await _messageManager.SendErrorMessage(context,
                "You must specify the title of the new category. Please try again.");
            return false;
        }

        if (!categoryTitle.Any(c => char.IsLetterOrDigit(c)))
        {
            await _messageManager.SendErrorMessage(context,
                "The category title must contain at least one letter or digit.");
            return false;
        }

        if (await _categoryManager.ExistsByTittle(context.Session.Id, categoryTitle, context.CancellationToken))
        {
            await _messageManager.SendErrorMessage(context,
                "A category with that title already exists.");
            return false;
        }
        return true;
    }
}
