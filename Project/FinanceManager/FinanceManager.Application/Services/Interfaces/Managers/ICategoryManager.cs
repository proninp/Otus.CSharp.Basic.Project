using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface ICategoryManager
{
    public Task<CategoryDto?> GetById(Guid id, CancellationToken cancellationToken);

    public Task<CategoryDto[]> Get(Guid userId, CancellationToken cancellationToken);

    public Task<CategoryDto> Create(CreateCategoryDto command, CancellationToken cancellationToken);

    public Task<CategoryDto> Update(UpdateCategoryDto command, CancellationToken cancellationToken);

    public Task Delete(Guid id, CancellationToken cancellationToken);
}