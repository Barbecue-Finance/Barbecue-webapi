using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DTOs.Misc;
using Models.DTOs.MoneyOperations;
using Models.DTOs.MoneyOperations.Transfers;
using Models.DTOs.Purses;

namespace Services.ApiServices.Abstractions
{
    public interface IMoneyOperationService
    {
        Task<CreatedDto> CreateIncome(CreateMoneyOperationDto createMoneyOperationDto);
        Task<CreatedDto> CreateOutCome(CreateMoneyOperationDto createMoneyOperationDto);

        Task<(CreatedDto outcomeId, CreatedDto incomeId)> CreateTransfer(CreateTransferOperationDto createTransferOperationDto);

        Task<IncomeOutcomeDto> GetByPurse(long id);
        
        Task UpdateIncome(MoneyOperationDto moneyOperationDto);
        Task UpdateOutCome(MoneyOperationDto moneyOperationDto);
    }
}