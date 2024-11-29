﻿using FinanceManager.Core.DataTransferObjects.Abstractions;
using FinanceManager.Core.Models;

namespace FinanceManager.Core.DataTransferObjects.Commands.Create;
public class CreateTransferDto : IPutModel<Transfer>
{
    public Guid UserId { get; init; }

    public Guid FromAccountId { get; set; }

    public Guid ToAccountId { get; set; }

    public DateTime Date { get; set; }

    public decimal FromAmount { get; set; }

    public decimal ToAmount { get; set; }

    public string? Description { get; set; }

    public Transfer ToModel() =>
        new Transfer(UserId, FromAccountId, ToAccountId, Date, FromAmount, ToAmount, Description);
}
