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

    public async Task<CategoryDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return (await _repository.GetByIdAsync(id, cancellationToken: cancellationToken))?.ToDto();
    }

    public Task<CategoryDto[]> Get(Guid userId, CancellationToken cancellationToken)
    {
        return _repository.GetAsync(
            c => c.ToDto(),
            c => c.UserId == userId,
            cancellationToken: cancellationToken);
    }

    public async Task<CategoryDto> Create(CreateCategoryDto command, CancellationToken cancellationToken)
    {
        var category = _repository.Add(command.ToModel());
        await _unitOfWork.CommitAsync(cancellationToken);
        return category.ToDto();
    }

    public async Task<CategoryDto> Update(UpdateCategoryDto command, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdOrThrowAsync(command.Id, cancellationToken: cancellationToken);

        category.Title = command.Title;
        category.ParentCategoryId = command.ParentCategoryId;

        _repository.Update(category);
        await _unitOfWork.CommitAsync(cancellationToken);
        return category.ToDto();
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdOrThrowAsync(id, cancellationToken: cancellationToken);
        _repository.Delete(category);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}