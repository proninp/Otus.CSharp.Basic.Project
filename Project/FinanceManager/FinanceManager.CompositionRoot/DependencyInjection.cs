﻿using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Application.Services.Managers;
using FinanceManager.Application.Services.Validators;
using FinanceManager.Bot.Application;
using FinanceManager.Core.Interfaces;
using FinanceManager.Core.Interfaces.Repositories;
using FinanceManager.Core.Options;
using FinanceManager.DataAccess.Data;
using FinanceManager.DataAccess.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.CompositionRoot;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
        services.AddDbContext<IUnitOfWork, AppDbContext>();

        services.AddHostedService<AppInitializer>();
        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserManager, UserManager>();
        services.AddScoped<IReadOnlyManager<CurrencyDto>, CurrencyManager>();
        services.AddScoped<IAccountManager, AccountManager>();
        services.AddScoped<ICategoryManager, CategoryManager>();
        services.AddScoped<ITransactionManager, TransactionManager>();
        services.AddScoped<ITransferManager, TransferManager>();
        services.AddScoped<ITransactionValidator, TransactionValidator>();
        services.AddScoped<IAccountValidator, AccountValidator>();

        services.AddTelegram(configuration);

        return services;
    }
}