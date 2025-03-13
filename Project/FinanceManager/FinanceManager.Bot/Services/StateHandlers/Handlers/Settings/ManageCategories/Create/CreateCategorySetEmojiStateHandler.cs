using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.Interfaces.Validators;
using FinanceManager.Bot.Services.StateHandlers.Contexts;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Settings.ManageCategories.Create;
public sealed class CreateCategorySetEmojiStateHandler : IStateHandler
{
    private readonly IUpdateMessageProvider _messageProvider;
    private readonly IMessageManager _messageManager;
    private readonly ICallbackDataProvider _callbackDataProvider;
    private readonly ISessionStateManager _sessionStateManager;
    private readonly IEmojiTextValidator _emojiTextValidator;

    public CreateCategorySetEmojiStateHandler(
        IUpdateMessageProvider messageProvider,
        IMessageManager messageManager,
        ICallbackDataProvider callbackDataProvider,
        ISessionStateManager sessionStateManager,
        IEmojiTextValidator emojiTextValidator)
    {
        _messageProvider = messageProvider;
        _messageManager = messageManager;
        _callbackDataProvider = callbackDataProvider;
        _sessionStateManager = sessionStateManager;
        _emojiTextValidator = emojiTextValidator;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        var context = updateContext.Session.GetCreateCategoryContext();

        await _messageManager.DeleteLastMessageAsync(updateContext);

        var callBackData = await _callbackDataProvider.GetCallbackData(updateContext, false);
        if (callBackData is not null)
        {
            if (callBackData.Data == Guid.Empty.ToString())
                return await _sessionStateManager.Next(updateContext.Session);
        }

        if (!_messageProvider.GetMessage(updateContext.Update, out var message))
            return await _sessionStateManager.Previous(updateContext.Session);

        if (message.Text is null || !_emojiTextValidator.ValidateSingleEmoji(message.Text))
        {
            await _messageManager.SendErrorMessageAsync(updateContext, "The specified text is not recognized as an emoji.");
            return await _sessionStateManager.Previous(updateContext.Session);
        }

        context.Emoji = message.Text;
        return await _sessionStateManager.Next(updateContext.Session);
    }
}
