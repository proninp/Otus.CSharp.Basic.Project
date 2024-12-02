using FinanceManager.Core.DataTransferObjects.Abstractions;
using FinanceManager.Core.Models;

namespace FinanceManager.Core.DataTransferObjects.ViewModels;

public sealed class AccountTypeDto : ViewDtoBase
{
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