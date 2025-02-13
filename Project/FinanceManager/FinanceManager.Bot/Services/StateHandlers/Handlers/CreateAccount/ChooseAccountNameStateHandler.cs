using System.Net;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.Providers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using FinanceManager.Bot.Services.Interfaces.Validators;
using FinanceManager.Bot.Services.StateHandlers.Contexts;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.CreateAccount;
public sealed class ChooseAccountNameStateHandler : IStateHandler
{
    private readonly IAccountManager _accountManager;
    private readonly IUpdateMessageProvider _messageProvider;
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;
    private readonly ITextSanitizer _textSanitizer;

    public ChooseAccountNameStateHandler(
        IAccountManager accountManager,
        IUpdateMessageProvider messageProvider,
        IMessageManager messageManager,
        ISessionStateManager sessionStateManager,
        ITextSanitizer textSanitizer)
    {
        _accountManager = accountManager;
        _messageProvider = messageProvider;
        _messageManager = messageManager;
        _sessionStateManager = sessionStateManager;
        _textSanitizer = textSanitizer;
    }

    public async Task<bool> HandleAsync(BotUpdateContext updateContext)
    {
        if (!_messageProvider.GetMessage(updateContext.Update, out var message))
            return false;

        if (!_textSanitizer.Sanitize(message.Text, out var accountTitle))
        {
            await _messageManager.SendErrorMessage(updateContext, "This account name is not allowed.");
            return false;
        }
        
        if (string.IsNullOrWhiteSpace(accountTitle) || accountTitle.Length == 0)
        {
            await _messageManager.SendErrorMessage(updateContext,
                "The account name must contain at least one non-whitespace character.");
            return false;
        }
        if (!char.IsLetterOrDigit(accountTitle[0]))
        {
            await _messageManager.SendErrorMessage(updateContext,
                "The account name must start with a number or letter. Enter a different account name.");
            return false;
        }

        accountTitle = WebUtility.HtmlEncode(accountTitle);

        var existingAccount = await _accountManager.GetByName(updateContext.Session.Id, accountTitle, false, updateContext.CancellationToken);
        if (existingAccount is not null)
        {
            await _messageManager.SendErrorMessage(updateContext,
                "An account with that name already exists. Enter a different name.");
            return false;
        }

        updateContext.Session.SetCreateAccountContext(new CreateAccountContext { AccountName = accountTitle });
        return await _sessionStateManager.Next(updateContext.Session);
    }
}