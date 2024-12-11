using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Application.DataTransferObjects.ViewModels;
using FinanceManager.Application.Services.Interfaces.Managers;
using FinanceManager.Core.Interfaces;
using FinanceManager.Core.Interfaces.Repositories;
using FinanceManager.Core.Models;

namespace FinanceManager.Application.Services.Managers;
public sealed class CategoryManager : ICategoryManager
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
        var category = _repository.Add(command.ToModel());
        await _unitOfWork.CommitAsync();
        return category.ToDto();
    }

    public async Task<CategoryDto> Update(UpdateCategoryDto command)
    {
        var category = await _repository.GetByIdOrThrow(command.Id);

        category.Title = command.Title;
        category.ParentCategoryId = command.ParentCategoryId;

        _repository.Update(category);
        await _unitOfWork.CommitAsync();
        return category.ToDto();
    }

    public async Task Delete(Guid id)
    {
        var category = await _repository.GetByIdOrThrow(id);
        _repository.Delete(category);
        await _unitOfWork.CommitAsync();
    }
}