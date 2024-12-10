using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;

namespace FinanceManager.Application.Services.Interfaces.Managers;
public interface ICategoryManager
{
    public Task<CategoryDto?> GetById(Guid id);

    public Task<CategoryDto[]> Get(Guid userId);

    public Task<CategoryDto> Create(CreateCategoryDto command);

    public Task<CategoryDto> Update(UpdateCategoryDto command);

    public Task Delete(Guid id);
}