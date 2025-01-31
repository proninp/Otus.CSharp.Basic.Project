using System.Text;
using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Application.Utils;
using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.Managers;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinanceManager.Bot.Services.StateHandlers.Handlers.Menu;
public class CreateMenuStateHandler : IStateHandler
{
    private readonly IAccountManager _accountManager;
    private readonly IMessageManager _messageManager;
    private readonly ISessionStateManager _sessionStateManager;

    public CreateMenuStateHandler(
        IMessageManager messageManager,
        IAccountManager accountManager,
        ISessionStateManager sessionStateManager)
    {
        _messageManager = messageManager;
        _accountManager = accountManager;
        _sessionStateManager = sessionStateManager;
    }

    public async Task HandleAsync(BotUpdateContext updateContext)
    {
        var account = await _accountManager.GetDefault(updateContext.Session.Id, updateContext.CancellationToken);
        if (account is null)
        {
            _sessionStateManager.Reset(updateContext.Session);
            return;
        }

        var messageText = await BuildMessageText(account, updateContext.CancellationToken);
        var inlineKeyboard = CreateInlineKeyboard(updateContext);

        await _messageManager.SendInlineKeyboardMessage(updateContext, messageText, inlineKeyboard);

        _sessionStateManager.Wait(updateContext.Session, WorkflowState.SelectMenu);
    }

    private async Task<string> BuildMessageText(AccountDto account, CancellationToken cancellationToken)
    {
        var balance = await _accountManager.GetBalance(account, cancellationToken);

        var messageBuilder = new StringBuilder($"{Emoji.IncomeAmount.GetSymbol()} Account: {account.Title}");
        messageBuilder.AppendLine();
        messageBuilder.Append($"{Emoji.Income.GetSymbol()} Balance: {balance}");
        if (account.Currency is not null)
            messageBuilder.Append($" {account.Currency.CurrencyCode} {account.Currency.Emoji}");
        messageBuilder.AppendLine();
        messageBuilder.Append("Choose an action:");

        return messageBuilder.ToString();
    }

    private InlineKeyboardMarkup CreateInlineKeyboard(BotUpdateContext context)
    {
        var keyboardButtons = new InlineKeyboardButton[][]
        {
            [
                _messageManager.CreateInlineButton(context, MainMenu.Expense.GetKey(),
                    $"{Emoji.Expense.GetSymbol()} {MainMenu.Expense.GetDescription()}"),
            
                _messageManager.CreateInlineButton(context, MainMenu.Income.GetKey(),
                    $"{Emoji.Income.GetSymbol()} {MainMenu.Income.GetDescription()}"),
            ],
            [
                _messageManager.CreateInlineButton(context, MainMenu.History.GetKey(),
                    $"{Emoji.History.GetSymbol()} {MainMenu.History.GetDescription()}"),

                _messageManager.CreateInlineButton(context, MainMenu.Settings.GetKey(),
                    $"{Emoji.Settings.GetSymbol()} {MainMenu.Settings.GetDescription()}"),
            ]
        };
        return new InlineKeyboardMarkup(keyboardButtons);
    }
}
