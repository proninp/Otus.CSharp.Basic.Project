using FinanceManager.Core.DataTransferObjects.Commands.Create;
using FinanceManager.Core.DataTransferObjects.Commands.Update;
using FinanceManager.Core.DataTransferObjects.ViewModels;
using FinanceManager.Core.Models;
using FinanceManager.Core.Services.Abstractions;
using FinanceManager.Core.Services.Abstractions.Managers;
using FinanceManager.Core.Services.Abstractions.Repositories;
using FinanceManager.Core.Services.Interfaces.Managers;

namespace FinanceManager.Core.Services.Managers;
public sealed class CategoryManager : ICategoryManager, IEntityProvider<Category>
{
    private readonly IRepository<Category> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryManager(IRepository<Category> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryDto?> GetById(Guid id)
    {
        return (await _repository.GetById(id))?.ToDto();
    }

    public Task<CategoryDto[]> Get(Guid userId)
    {
        return _repository.Get(c => c.UserId == userId, c => c.ToDto());
    }

    public async Task<CategoryDto> Create(CreateCategoryDto command)
    {
        var model = _repository.Add(command.ToModel());
        await _unitOfWork.Commit();
        return model.ToDto();
    }

    public async Task<CategoryDto> Update(UpdateCategoryDto command)
    {
        var entityProvider = (IEntityProvider<Category>)this; // TODO Questionable: IEntityProvider
        var category = await entityProvider.GetEntityById(_repository, command.Id);

        category.Title = command.Title;
        category.ParentCategoryId = command.ParentCategoryId;

        _repository.Update(category);
        await _unitOfWork.Commit();
        return category.ToDto();
    }

    public async Task Delete(Guid id)
    {
        var entityProvider = (IEntityProvider<Category>)this; // TODO Questionable: IEntityProvider
        var category = await entityProvider.GetEntityById(_repository, id);
        _repository.Delete(category);
        await _unitOfWork.Commit();
    }
}