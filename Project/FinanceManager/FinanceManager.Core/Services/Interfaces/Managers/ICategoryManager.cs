using FinanceManager.Core.DataTransferObjects.Commands.Create;
using FinanceManager.Core.DataTransferObjects.Commands.Update;
using FinanceManager.Core.DataTransferObjects.ViewModels;

namespace FinanceManager.Core.Services.Abstractions.Managers;
public interface ICategoryManager
{
    public Task<CategoryDto?> GetById(Guid id);

    public Task<CategoryDto[]> Get(Guid userId);

    public Task<CategoryDto> Create(CreateCategoryDto command);

    public Task<CategoryDto> Update(UpdateCategoryDto command);

    public Task Delete(Guid id);
}
