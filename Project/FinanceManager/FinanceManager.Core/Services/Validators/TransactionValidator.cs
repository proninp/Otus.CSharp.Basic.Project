using FinanceManager.Core.DataTransferObjects.Commands.Create;
using FinanceManager.Core.DataTransferObjects.Commands.Update;
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

    // ITransactionCommand - общий интерфейс с общими полями для транзакции и валидатор принимает его

    public void Validate(CreateTransactionDto command)
    {
        CheckCommand(command.Amount, command.TransactionType);
        Validate(command.ToModel());
    }

    public void Validate(UpdateTransactionDto command)
    {
        CheckCommand(command.Amount, command.TransactionType);
        Validate(command.ToModel()); // TODO валидировать только команду, моель валидировать не нужно
    }

    private void Validate(Transaction model)
    {
        var account = _accountManager.GetById(model.AccountId);
        if (account is null)
            throw new ArgumentException("В транзакции не указан счет.");
    }

    private void CheckCommand(decimal amount, TransactionType transactionType)
    {
        if (amount <= 0)
            throw new ArgumentException("Операция может быть зарегистрирована только для положительного значения суммы.");

        if (transactionType is not (TransactionType.Income or TransactionType.Expense))
            throw new ArgumentException("Неизвестный тип транзакциии.");
    }
}
