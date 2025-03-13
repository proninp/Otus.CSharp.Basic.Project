using FinanceManager.Application.DataTransferObjects.Abstractions;

namespace FinanceManager.Application.Services.Interfaces;
public interface ITransactionValidator
{
    Task ValidateAsync(ITransactionableCommand transactionCommand, CancellationToken cancellationToken);
}