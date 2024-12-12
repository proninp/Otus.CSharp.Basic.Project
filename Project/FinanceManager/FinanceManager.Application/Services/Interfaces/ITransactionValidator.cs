using FinanceManager.Application.DataTransferObjects.Abstractions;

namespace FinanceManager.Application.Services.Interfaces;
public interface ITransactionValidator
{
    Task Validate(ITransactionableCommand transactionCommand);
}