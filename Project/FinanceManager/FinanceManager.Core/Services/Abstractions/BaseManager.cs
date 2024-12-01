using FinanceManager.Core.DataTransferObjects.Abstractions;
using FinanceManager.Core.Models.Abstractions;
using FinanceManager.Core.Services.Abstractions.Repositories;

namespace FinanceManager.Core.Services.Abstractions;
public abstract class BaseManager<T, TViewDto, TCreateDto, TUpdateDto>
    where T : BaseModel
    where TViewDto : BaseViewDto
    where TCreateDto : IPutModel<T>
    where TUpdateDto : BaseUpdateDto<T>
{
    protected readonly IRepository<T> _repository;
    protected readonly IUnitOfWork _unitOfWork;
    
    public event Action<TCreateDto>? OnBeforeCreate;
    public event Action<TUpdateDto>? OnBeforeUpdate;

    public BaseManager(IRepository<T> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TViewDto> Create(TCreateDto command)
    {
        if (OnBeforeCreate is not null)
            OnBeforeCreate.Invoke(command);

        var model = _repository.Add(command.ToModel());
        await _unitOfWork.Commit();
        return GetViewDto(model);
    }

    public virtual async Task<TViewDto> Update(TUpdateDto command)
    {
        if (OnBeforeUpdate is not null)
            OnBeforeUpdate.Invoke(command);

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
