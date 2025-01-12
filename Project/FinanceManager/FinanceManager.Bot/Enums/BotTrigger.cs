namespace FinanceManager.Bot.Enums;
public enum BotTrigger
{
    Start,
    StartedWithoutAccount,
    EnterInputAccountName,
    AccountNameEntered,
    SendCurrencies,
    CurrencyChosen,
    AccountCreationCompleted,
    StartedWithAccount,
    EnterCreateExpense,
    EnterCreateIncome,
    ChooseCategory,
    TransactionDateSelected,
    TransactionAmountEntered,
    TransactionComplete,
    AccountComplete,
    BackToDefault
}
