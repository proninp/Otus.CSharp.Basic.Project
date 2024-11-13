using FinanceManager.Core.Models;

namespace FinanceManager.Core.DataTransferObjects.ViewModels;

public class AccountTypeDto
{
    public Guid Id { get; init; }

    public string Name { get; init; }
}

public static class AccountTypeMappings
{
    public static AccountTypeDto ToDto(this AccountType accountType)
    {
        return new AccountTypeDto
        {
            Id = accountType.Id,
            Name = accountType.Name
        };
    }
}