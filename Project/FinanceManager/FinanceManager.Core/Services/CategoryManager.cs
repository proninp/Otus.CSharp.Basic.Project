using FinanceManager.Core.DataTransferObjects.Commands;
using FinanceManager.Core.DataTransferObjects.ViewModels;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions;

namespace FinanceManager.Core.Services;
public class CategoryManager : BaseManager<Category, PutCategoryDto>
{
    public CategoryManager(IRepository<Category> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
    }

    public async Task<CategoryDto?> GetById(Guid id)
    {
        return (await _repository.GetById(id))?.ToDto();
    }

    public async Task<CategoryDto[]> GetCategories(Guid userId)
    {
        return await _repository.Get(c => c.UserId == userId, c => c.ToDto());
    }

    protected override void Update(Category category, PutCategoryDto command)
    {
        category.Title = command.Title;
        category.ParentCategoryId = command.ParentCategoryId;
    }
}