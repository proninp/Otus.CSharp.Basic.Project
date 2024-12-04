using FinanceManager.Core.DataTransferObjects;
using FinanceManager.Core.DataTransferObjects.ViewModels;
using FinanceManager.Core.Options;
using FinanceManager.Core.Services;
using FinanceManager.Core.Services.Abstractions;
using FinanceManager.Core.Services.Abstractions.Interfaces;
using FinanceManager.Core.Services.Abstractions.Managers;
using FinanceManager.Core.Services.Abstractions.Repositories;
using FinanceManager.Core.Services.Managers;
using FinanceManager.Core.Services.Validators;
using FinanceManager.Infrastructure.Data;
using FinanceManager.Infrastructure.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Application;

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
        services.AddScoped<IReadOnlyManager<AccountTypeDto>, AccountTypeManager>();
        services.AddScoped<IReadOnlyManager<CurrencyDto>, CurrencyManager>();
        services.AddScoped<IAccountManager, AccountManager>();
        services.AddScoped<ICategoryManager, CategoryManager>();
        services.AddScoped<ITransactionManager, TransactionManager>();
        services.AddScoped<ITransferManager, TransferManager>();
        services.AddScoped<ITransactionValidator, TransactionValidator>();
        services.AddScoped<FinanceService>();

        services.AddTelegram(configuration);

        return services;
    }
}
