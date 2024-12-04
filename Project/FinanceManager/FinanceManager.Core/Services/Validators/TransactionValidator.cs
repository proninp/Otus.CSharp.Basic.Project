using FinanceManager.Core.DataTransferObjects.Abstractions;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions;
using FinanceManager.Core.Services.Abstractions.Managers;

namespace FinanceManager.Core.Services.Validators;
public class TransactionValidator : ITransactionValidator
{
    private readonly IAccountManager _accountManager;

    public TransactionValidator(IAccountManager accountManager)
    {
        _accountManager = accountManager;
    }

    public void Validate(ITransactionableCommand transactionCommand)
    {
        if (transactionCommand.Amount <= 0)
            throw new ArgumentException("Операция может быть зарегистрирована только для положительного значения суммы.");

        if (transactionCommand.TransactionType is not (TransactionType.Income or TransactionType.Expense))
            throw new ArgumentException("Неизвестный тип транзакциии.");

        var account = _accountManager.GetById(transactionCommand.AccountId);
        if (account is null)
            throw new ArgumentException("В транзакции не указан счет.");
    }
}
