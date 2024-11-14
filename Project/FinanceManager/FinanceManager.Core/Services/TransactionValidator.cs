using FinanceManager.Core.DataTransferObjects.Commands;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions;

namespace FinanceManager.Core.Services;
public class TransactionValidator : ITransactionValidator
{
    public void Validate(PutTransactionDto command, Transaction? transaction)
    {
        if (command.Amount < 0)
            throw new ArgumentException("Операция не может быть зарегистрирован с отрицательной суммой");

        if (command.Id is not null)
        {
            if (transaction is null)
                throw new ArgumentNullException($"Транзакция с id {command.Id.Value} не найдена.");

            if (transaction.TransactionType != command.TransactionType)
                throw new ArgumentException("Нельзя изменить тип транзакции.");
        }

        if (command.TransactionType is not (TransactionType.Income or TransactionType.Expense))
            throw new ArgumentException("Неизвестный тип транзакциии.");
    }
}
