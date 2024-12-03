using FinanceManager.Core.DataTransferObjects.Abstractions;
using FinanceManager.Core.Models.Abstractions;
using FinanceManager.Core.Services.Abstractions.Repositories;

namespace FinanceManager.Core.Services.Abstractions.Managers;
public abstract class BaseManager<T, TViewDto, TCreateDto, TUpdateDto>
    where T : BaseModel
    where TViewDto : IdentityDtoBase
    where TCreateDto : IPutModel<T>
    where TUpdateDto : UpdateDtoBase<T>
{
    protected readonly IRepository<T> _repository;
    protected readonly IUnitOfWork _unitOfWork;

    // TODO Убрать BaseManager и перенести всё в отдельные сервисы Transaction, Category, Transfer и т.д.

    public BaseManager(IRepository<T> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public virtual async Task<TViewDto> Create(TCreateDto command)
    {
        var model = _repository.Add(command.ToModel());
        await _unitOfWork.Commit();
        return GetViewDto(model);
    }

    public virtual async Task<TViewDto> Update(TUpdateDto command)
    {
        var model = await GetEntityById(command.Id);
        UpdateModel(model, command);
        _repository.Update(model);
        await _unitOfWork.Commit();
        return GetViewDto(model);
    }

    public async Task Delete(Guid id)
    {
        var entry = await GetEntityById(id);
        _repository.Delete(entry);
        await _unitOfWork.Commit();
    }

    protected async Task<T> GetEntityById(Guid id)
    {
        var entry = await _repository.GetById(id);
        if (entry is null)
            throw new ArgumentException($"Запись {typeof(T).Name} с id:'{id}' не была найдена.");
        return entry;
    }

    protected abstract TViewDto GetViewDto(T model);

    protected abstract void UpdateModel(T model, TUpdateDto command);
}
