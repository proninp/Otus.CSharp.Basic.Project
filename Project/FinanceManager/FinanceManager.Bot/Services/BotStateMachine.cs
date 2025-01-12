using FinanceManager.Bot.Enums;
using FinanceManager.Bot.Models;
using FinanceManager.Bot.Services.Interfaces.StateHandlers;
using Stateless;

namespace FinanceManager.Bot.Services;
public class BotStateMachine
{
    private readonly StateMachine<BotState, BotTrigger> _stateMachine;
    private readonly IStateHandlerFactory _handlerFactory;

    public BotStateMachine(IStateHandlerFactory handlerFactory, StateMachine<BotState, BotTrigger> stateMachine)
    {
        _handlerFactory = handlerFactory;
        _stateMachine = stateMachine;

        _stateMachine.SetTriggerParameters<UpdateHandlerCommand>(BotTrigger.Start);
        
        
        _stateMachine.SetTriggerParameters<UpdateHandlerCommand>(BotTrigger.EnterCreateExpense);
        _stateMachine.SetTriggerParameters<UpdateHandlerCommand>(BotTrigger.EnterCreateIncome);
        _stateMachine.SetTriggerParameters<UpdateHandlerCommand>(BotTrigger.ChooseCategory);
        _stateMachine.SetTriggerParameters<UpdateHandlerCommand>(BotTrigger.TransactionDateSelected);
        _stateMachine.SetTriggerParameters<UpdateHandlerCommand>(BotTrigger.TransactionAmountEntered);
        _stateMachine.SetTriggerParameters<UpdateHandlerCommand>(BotTrigger.TransactionComplete);
        _stateMachine.SetTriggerParameters<UpdateHandlerCommand>(BotTrigger.AccountComplete);
        _stateMachine.SetTriggerParameters<UpdateHandlerCommand>(BotTrigger.BackToDefault);

        ConfigureDefaultState();
        
    }

    private void ConfigureDefaultState()
    {
        var noDefautlAccountTrigger = _stateMachine.SetTriggerParameters<UpdateHandlerCommand>(BotTrigger.StartedWithoutAccount);
        var defaultAccountExistsTrigger = _stateMachine.SetTriggerParameters<UpdateHandlerCommand>(BotTrigger.StartedWithAccount);

        _stateMachine.Configure(BotState.Default)
            .Permit(BotTrigger.StartedWithoutAccount, BotState.CreateAccountStart)
            .Permit(BotTrigger.StartedWithAccount, BotState.Menu)
            .OnEntry(async () => await _handlerFactory.GetHandler(BotState.CreateAccountStart).HandleAsync(command))
            .OnEntryFromAsync(defaultAccountExistsTrigger,
                async (command) => await _handlerFactory.GetHandler(BotState.Menu).HandleAsync(command));
    }

    private void ConfigureCreateAccountState()
    {
        var started = _stateMachine.SetTriggerParameters<UpdateHandlerCommand>(BotTrigger.StartedWithoutAccount);
        var accountNameEntered = _stateMachine.SetTriggerParameters<UpdateHandlerCommand>(BotTrigger.AccountNameEnteredOnCreateAccount);
        var currencyChosen = _stateMachine.SetTriggerParameters<UpdateHandlerCommand>(BotTrigger.CurrencyChosenOnCreateAccount);
        var creationCompleted = _stateMachine.SetTriggerParameters<UpdateHandlerCommand>(BotTrigger.AccountCreationCompleted);

        _stateMachine.Configure(BotState.CreateAccountStart)
            .OnEntryAsync(async () => await _handlerFactory.GetHandler(BotState.CreateAccountStart).HandleAsync())
            .Permit(BotTrigger.EnterInputAccountName, BotState.ChooseAccountName);

        _stateMachine.Configure(BotState.ChooseAccountName)
            .OnEntryAsync(async () => await HandleChooseAccountName())
            .Permit(BotTrigger.EnterCreateAccount, BotState.SendCurrencies);

        _stateMachine.Configure(BotState.SendCurrencies)
            .OnEntryAsync(async () => await HandleSendCurrencies())
            .Permit(BotTrigger.EnterCreateAccount, BotState.ChooseCurrency);

        _stateMachine.Configure(BotState.ChooseCurrency)
            .OnEntryAsync(async () => await HandleChooseCurrency())
            .Permit(BotTrigger.EnterCreateAccount, BotState.SetAccountBalance);

        _stateMachine.Configure(BotState.SetAccountBalance)
            .OnEntryAsync(async () => await HandleSetAccountBalance())
            .Permit(BotTrigger.EnterCreateAccount, BotState.Default);

        _stateMachine.Configure(BotState.CreateAccountEnd)
            .OnEntryAsync(async () => await HandleCreateAccountEnd())
            .Permit(BotTrigger.AccountComplete, BotState.Default);
    }
}
