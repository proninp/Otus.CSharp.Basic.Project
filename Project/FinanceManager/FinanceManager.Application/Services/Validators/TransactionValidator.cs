using FinanceManager.Application.DataTransferObjects.Abstractions;
using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces;

namespace FinanceManager.Application.Services.Validators;
public class TransactionValidator : ITransactionValidator
{
    private readonly IAccountValidator _accountValidator;

    public TransactionValidator(IAccountValidator accountValidator)
    {
        _accountValidator = accountValidator;
    }

    public async Task Validate(ITransactionableCommand transactionCommand, CancellationToken cancellationToken)
    {
        if (transactionCommand.Amount <= 0)
            throw new ArgumentException("Операция может быть зарегистрирована только для положительного значения суммы.");

        if (transactionCommand.TransactionType is not (TransactionType.Income or TransactionType.Expense))
            throw new ArgumentException("Неизвестный тип транзакциии.");

        if (!(await _accountValidator.AccountExists(transactionCommand.AccountId, cancellationToken)))
            throw new ArgumentException("В транзакции не указан счет.");
    }
}
