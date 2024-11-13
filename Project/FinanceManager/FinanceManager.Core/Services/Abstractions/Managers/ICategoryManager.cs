using FinanceManager.Core.DataTransferObjects.Commands;
using FinanceManager.Core.DataTransferObjects.ViewModels;

namespace FinanceManager.Core.Services.Abstractions.Managers;
public interface ICategoryManager
{
    public Task<CategoryDto?> GetById(Guid id);

    public Task<CategoryDto[]> Get(Guid userId);

    public Task Put(PutCategoryDto command);

    public Task Delete(Guid id);
}
