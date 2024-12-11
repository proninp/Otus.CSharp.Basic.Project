using FinanceManager.Application.DataTransferObjects.Abstractions;
using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces;
using FinanceManager.Core.Interfaces.Repositories;
using FinanceManager.Core.Models;

namespace FinanceManager.Application.Services.Validators;
public class TransactionValidator : ITransactionValidator
{
    private readonly IRepository<Account> _repository;

    public TransactionValidator(IRepository<Account> repository)
    {
        _repository = repository;
    }

    public void Validate(ITransactionableCommand transactionCommand)
    {
        if (transactionCommand.Amount <= 0)
            throw new ArgumentException("Операция может быть зарегистрирована только для положительного значения суммы.");

        if (transactionCommand.TransactionType is not (TransactionType.Income or TransactionType.Expense))
            throw new ArgumentException("Неизвестный тип транзакциии.");

        var account = _repository.GetById(transactionCommand.AccountId);
        if (account is null)
            throw new ArgumentException("В транзакции не указан счет.");
    }
}
