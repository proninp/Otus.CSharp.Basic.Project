using FinanceManager.Core.DataTransferObjects.Commands;
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

    public void Validate(PutTransactionDto command)
    {
        if (command.Amount < 0)
            throw new ArgumentException("Операция не может быть зарегистрирован с отрицательной суммой");

        var accountTpe = _accountManager.GetById(command.AccountId);
        if (accountTpe is null)
            throw new ArgumentException("Невозможно зарегистрировать транзакцию: не указан счет.");
    }
}
