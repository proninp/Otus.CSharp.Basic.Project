
namespace FinanceManager.Application.Services.Interfaces;
public interface ICategoriesInitializer
{
    Task InitializeDefaultsAsync(Guid userId, CancellationToken cancellationToken);
}
