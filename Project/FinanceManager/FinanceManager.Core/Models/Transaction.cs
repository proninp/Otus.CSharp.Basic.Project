﻿using FinanceManager.Core.Models.Abstractions;

namespace FinanceManager.Core.Models;

public sealed class Transaction : BaseModel
{
    public Guid UserId { get; init; }

    public Guid AccountId { get; set; }

    public Guid? CategoryId { get; set; }

    public DateOnly Date { get; set; }

    public TransactionType TransactionType { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public User User { get; }

    public Account Account { get; set; }

    public Category? Category { get; set; }

    protected Transaction() { }

    public Transaction(Guid userId, Guid accountId, Guid? categoryId = null, DateOnly? date = null, TransactionType transactionType = TransactionType.Expense, decimal amount = 0, string? description = null)
    {
        UserId = userId;
        AccountId = accountId;
        CategoryId = categoryId;
        Date = date ?? DateOnly.FromDateTime(DateTime.UtcNow);
        TransactionType = transactionType;
        Amount = amount;
        Description = description;
    }
}
