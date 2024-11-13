using FinanceManager.Core.DataTransferObjects.ViewModels;
using FinanceManager.Core.Services.Abstractions.Managers;

namespace FinanceManager.Core.Services;
public sealed class FinanceService(
    IAccountManager accountManager,
    ICategoryManager categoryManager,
    ITransactionManager transactionManager,
    ITransferManager transferManager)
{
    public async Task<FinanceViewModel> GetUserFinanceData(Guid userId)
    {
        var accounts = await accountManager.Get(userId);
        var categories = await categoryManager.Get(userId);
        var transactions = await transactionManager.Get(userId);
        var transfers = await transferManager.Get(userId);
        

        return new FinanceViewModel
        {
            Accounts = accounts,
            Categories = categories,
            Transactions = transactions,
            Transfers = transfers
        };
    }
}
