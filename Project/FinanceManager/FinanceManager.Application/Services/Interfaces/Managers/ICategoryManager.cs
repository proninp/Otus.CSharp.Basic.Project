using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface ICategoryManager
{
    Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<CategoryDto[]> GetAsync(Guid userId, CancellationToken cancellationToken);

    Task<CategoryDto[]> GetExpensesAsync(Guid userId, CancellationToken cancellationToken);

    Task<CategoryDto[]> GetIncomesAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> ExistsByTittleAsync(Guid userId, string title, CancellationToken cancellationToken);

    Task<CategoryDto> CreateAsync(CreateCategoryDto command, CancellationToken cancellationToken);

    Task<CategoryDto> UpdateAsync(UpdateCategoryDto command, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}