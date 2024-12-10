using FinanceManager.Application.DataTransferObjects.Abstractions;
using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces;
using FinanceManager.Application.Services.Interfaces.Managers;

namespace FinanceManager.Application.Services.Validators;
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
