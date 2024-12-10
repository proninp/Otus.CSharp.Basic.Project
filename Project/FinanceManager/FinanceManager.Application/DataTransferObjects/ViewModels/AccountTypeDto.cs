using FinanceManager.Application.DataTransferObjects.Abstractions;
using FinanceManager.Core.Models;

namespace FinanceManager.Application.DataTransferObjects.ViewModels;
public sealed class AccountTypeDto : IdentityDtoBase
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