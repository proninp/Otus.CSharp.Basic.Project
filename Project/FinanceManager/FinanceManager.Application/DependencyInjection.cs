using FinanceManager.Core.Options;
using FinanceManager.Core.Services;
using FinanceManager.Core.Services.Abstractions;
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
        services.AddScoped<UserManager>();
        services.AddScoped<AccountManager>();
        services.AddScoped<CategoryManager>();
        services.AddScoped<TransactionManager>();
        services.AddScoped<TransferManager>();
        services.AddScoped<FinanceService>();

        return services;
    }
}
