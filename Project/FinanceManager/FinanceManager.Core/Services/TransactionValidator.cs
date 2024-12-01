using FinanceManager.Core.DataTransferObjects.Abstractions;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions;
using FinanceManager.Core.Services.Abstractions.Managers;

namespace FinanceManager.Core.Services;
public class TransactionValidator : ITransactionValidator
{
    private readonly IAccountManager _accountManager;

    public TransactionValidator(IAccountManager accountManager)
    {
        _accountManager = accountManager;
    }

    public void Validate(IPutModel<Transaction> command)
    {
        var model = command.ToModel();
        if (model.Amount <= 0)
            throw new ArgumentException("Операция может быть зарегистрирована только для положительного значения суммы.");

        if (model.TransactionType is not (TransactionType.Income or TransactionType.Expense))
            throw new ArgumentException("Неизвестный тип транзакциии.");

        // TODO может быть отловлено самой БД через Foreign Key ?
        var account = _accountManager.GetById(model.AccountId);
        if (account is null)
            throw new ArgumentException("В транзакции не указан счет.");
    }
}
