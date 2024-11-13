using FinanceManager.Core.DataTransferObjects.Commands.Abstractions;
using FinanceManager.Core.Models.Abstractions;

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

    public async Task PutEntry(TDto command)
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

    public async Task DeleteEntry(Guid id)
    {
        var entry = await GetEntityById(id);
        _repository.Delete(entry);
        await _unitOfWork.Commit();
    }

    private async Task<T> GetEntityById(Guid id)
    {
        var entry = await _repository.GetById(id);
        if (entry is null)
            throw new ArgumentException($"{typeof(T).Name} with id {id} was not found");
        return entry;
    }

    protected abstract void Update(T model, TDto command);
}
