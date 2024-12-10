using AutoMapper;
using FinanceManager.Application.DataTransferObjects.Commands.Create;
using FinanceManager.Application.DataTransferObjects.Commands.Update;
using FinanceManager.Core.Models;

namespace FinanceManager.Application.MappingProfiles;
public class AccountMappingProfile : Profile
{
    public AccountMappingProfile()
    {
        CreateMap<CreateAccountDto, Account>();
        CreateMap<UpdateAccountDto, Account>();
    }
}
