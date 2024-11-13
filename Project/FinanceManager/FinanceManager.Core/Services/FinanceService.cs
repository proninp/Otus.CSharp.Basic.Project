﻿using FinanceManager.Core.DataTransferObjects.ViewModels;

namespace FinanceManager.Core.Services;
public sealed class FinanceService(
    AccountManager accountManager,
    CategoryManager categoryManager,
    TransactionManager transactionManager,
    TransferManager transferManager)
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
