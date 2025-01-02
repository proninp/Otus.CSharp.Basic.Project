using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface ICategoryManager
{
    Task<CategoryDto?> GetById(Guid id, CancellationToken cancellationToken);

    Task<CategoryDto[]> Get(Guid userId, CancellationToken cancellationToken);
    
    Task<bool> Exists(Guid userId, CancellationToken cancellationToken);

    Task<CategoryDto> Create(CreateCategoryDto command, CancellationToken cancellationToken);

    Task<CategoryDto> Update(UpdateCategoryDto command, CancellationToken cancellationToken);

    Task Delete(Guid id, CancellationToken cancellationToken);
}