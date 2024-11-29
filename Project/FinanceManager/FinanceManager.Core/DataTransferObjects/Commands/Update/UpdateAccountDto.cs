using FinanceManager.Core.DataTransferObjects.Abstractions;
using FinanceManager.Core.Models;

namespace FinanceManager.Core.DataTransferObjects.Commands.Update;
public class UpdateAccountDto : BaseUpdateDto<Account>
{
    public Guid UserId { get; init; }

    public Guid AccountTypeId { get; init; }

    public Guid CurrencyId { get; init; }

    public string? Title { get; set; }

    public decimal Balance { get; set; }

    public bool IsDefault { get; set; }

    public bool IsArchived { get; set; }

    public override Account ToModel() =>
        new Account(UserId, AccountTypeId, CurrencyId, Title, Balance, IsDefault, IsArchived);
}
