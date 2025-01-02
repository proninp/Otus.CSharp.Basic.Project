
namespace FinanceManager.Application.Services.Interfaces;
public interface ICategoriesInitializer
{
    Task InitializeDefaults(Guid userId, CancellationToken cancellationToken);
}
