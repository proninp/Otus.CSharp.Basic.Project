using FinanceManager.Core.DataTransferObjects.Abstractions;
using FinanceManager.Core.Models;

namespace FinanceManager.Core.DataTransferObjects.Commands.Update;
public class UpdateCategoryDto : BaseUpdateDto<Category>, IPutModel<Category>
{
    public Guid UsertId { get; init; }

    public string? Title { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public override Category ToModel() =>
        new Category(UsertId, Title, ParentCategoryId);
}
