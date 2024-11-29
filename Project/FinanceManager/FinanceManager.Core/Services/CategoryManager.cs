using FinanceManager.Core.DataTransferObjects.Commands.Create;
using FinanceManager.Core.DataTransferObjects.Commands.Update;
using FinanceManager.Core.DataTransferObjects.ViewModels;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions;
using FinanceManager.Core.Services.Abstractions.Managers;
using FinanceManager.Core.Services.Abstractions.Repositories;

namespace FinanceManager.Core.Services;
public class CategoryManager : BaseManager<Category, CategoryDto, CreateCategoryDto, UpdateCategoryDto>, ICategoryManager
{
    public CategoryManager(IRepository<Category> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
    }

    public async Task<CategoryDto?> GetById(Guid id)
    {
        return (await _repository.GetById(id))?.ToDto();
    }

    public async Task<CategoryDto[]> Get(Guid userId)
    {
        return await _repository.Get(c => c.UserId == userId, c => c.ToDto());
    }

    protected override void UpdateModel(Category category, UpdateCategoryDto command)
    {
        category.Title = command.Title;
        category.ParentCategoryId = command.ParentCategoryId;
    }

    protected override CategoryDto GetViewDto(Category model) =>
        model.ToDto();
}