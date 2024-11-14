using FinanceManager.Core.DataTransferObjects.Commands.Abstractions;
using FinanceManager.Core.Models.Abstractions;
using FinanceManager.Core.Services.Abstractions.Repositories;

namespace FinanceManager.Core.Services.Abstractions;
public abstract class BaseManager<T, TDto>
    where T : BaseModel
    where TDto : BasePutDto<T>
{
    protected IRepository<T> _repository;

    protected IUnitOfWork _unitOfWork;

    public BaseManager(IRepository<T> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public virtual async Task Put(TDto command)
    {
        if (command.Id is null)
        {
            _repository.Add(command.ToModel());
        }
        else
        {
            var entity = await GetEntityById(command.Id.Value);
            Update(entity, command);
        }
        await _unitOfWork.Commit();
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

    protected abstract void Update(T model, TDto command);
}
