using System.Net;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Validators;

namespace FinanceManager.Bot.Services.StateHandlers.Validators;
public class CategoryTitleValidator : ICategoryTitleValidator
{
    private readonly IMessageManager _messageManager;
    private readonly ICategoryManager _categoryManager;
    private readonly ITextSanitizer _textSanitizer;

    public CategoryTitleValidator(
        IMessageManager messageManager, ICategoryManager categoryManager, ITextSanitizer textSanitizer)
    {
        _messageManager = messageManager;
        _textSanitizer = textSanitizer;
        _categoryManager = categoryManager;
    }

    public async Task<(bool isValid, string newTitle)> ValidateAsync(BotUpdateContext context, string? categoryTitle)
    {
        if (!_textSanitizer.Sanitize(categoryTitle, out var title))
        {
            await _messageManager.SendErrorMessageAsync(context,
                "This category name is not allowed. Please try again.");
            return (false, title);
        }

        if (string.IsNullOrEmpty(title))
        {
            await _messageManager.SendErrorMessageAsync(context,
                "You must specify the title for the category. Please try again.");
            return (false, title);
        }

        if (!title.Any(c => char.IsLetterOrDigit(c)))
        {
            await _messageManager.SendErrorMessageAsync(context,
                "The category title must contain at least one letter or digit. Please try again.");
            return (false, title);
        }

        if (await _categoryManager.ExistsByTittleAsync(context.Session.Id, title, context.CancellationToken))
        {
            await _messageManager.SendErrorMessageAsync(context,
                "A category with that title already exists. Please try again.");
            return (false, title);
        }
        return (true, title);
    }
}
