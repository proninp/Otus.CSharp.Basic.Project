using FinanceManager.Application.DataTransferObjects.Abstractions;

namespace FinanceManager.Application.DataTransferObjects.Commands.Update;
public sealed class UpdateCategoryDto : IdentityDtoBase
{
    public Guid UsertId { get; init; }

    public string? Title { get; set; }

    public string? Emoji { get; set; }

    public Guid? ParentCategoryId { get; set; }
}