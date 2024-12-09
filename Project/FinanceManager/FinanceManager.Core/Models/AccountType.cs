﻿using FinanceManager.Core.Models.Abstractions;

namespace FinanceManager.Core.Models;
public sealed class AccountType : IdentityModel
{
    public string Name { get; init; }

    public AccountType(string name)
    {
        Name = name;
    }
}