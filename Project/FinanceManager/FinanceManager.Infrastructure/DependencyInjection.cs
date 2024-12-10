using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Application.Services.Managers;
using FinanceManager.Application.Services.Validators;
using FinanceManager.Core.Interfaces;
using FinanceManager.Core.Interfaces.Repositories;
using FinanceManager.Core.Options;
using FinanceManager.Persistence.Data;
using FinanceManager.Persistence.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Infrastructure;
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

        //services.AddTelegram(configuration);

        return services;
    }
}