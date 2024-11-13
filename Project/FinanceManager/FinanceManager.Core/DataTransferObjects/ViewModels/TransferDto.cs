using FinanceManager.Core.Models;

namespace FinanceManager.Core.DataTransferObjects.ViewModels;
public class TransferDto
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public Guid FromAccountId { get; set; }

    public Guid ToAccountId { get; set; }

    public DateTime Date { get; set; }

    public decimal FromAmount { get; set; }

    public decimal ToAmount { get; set; }

    public string? Description { get; set; }
}

public static class TransferMappings
{
    public static TransferDto ToDto(this Transfer transfer)
    {
        return new TransferDto
        {
            Id = transfer.Id,
            UserId = transfer.UserId,
            FromAccountId = transfer.FromAccountId,
            ToAccountId = transfer.ToAccountId,
            Date = transfer.Date,
            FromAmount = transfer.FromAmount,
            ToAmount = transfer.ToAmount,
            Description = transfer.Description
        };
    }
}
