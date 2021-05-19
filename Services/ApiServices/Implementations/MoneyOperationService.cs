using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using Models.Db.MoneyOperations;
using Models.DTOs.Misc;
using Models.DTOs.MoneyOperations;
using Models.DTOs.MoneyOperations.Transfers;
using Models.DTOs.Purses;
using Services.ApiServices.Abstractions;

namespace Services.ApiServices.Implementations
{
    public class MoneyOperationService : IMoneyOperationService
    {
        private readonly IIncomeMoneyOperationRepository _incomeMoneyOperationRepository;
        private readonly IOutComeMoneyOperationRepository _outComeMoneyOperationRepository;
        private readonly IPurseRepository _purseRepository;

        private readonly IMapper _mapper;

        public MoneyOperationService(IIncomeMoneyOperationRepository incomeMoneyOperationRepository, IOutComeMoneyOperationRepository outComeMoneyOperationRepository, IMapper mapper, IPurseRepository purseRepository)
        {
            _incomeMoneyOperationRepository = incomeMoneyOperationRepository;
            _outComeMoneyOperationRepository = outComeMoneyOperationRepository;
            _mapper = mapper;
            _purseRepository = purseRepository;
        }

        public async Task<CreatedDto> CreateIncome(CreateMoneyOperationDto createMoneyOperationDto)
        {
            var incomeMoneyOperation = _mapper.Map<IncomeMoneyOperation>(createMoneyOperationDto);

            await _incomeMoneyOperationRepository.Add(incomeMoneyOperation);

            return incomeMoneyOperation.Id;
        }

        public async Task<CreatedDto> CreateOutCome(CreateMoneyOperationDto createMoneyOperationDto)
        {
            var outComeMoneyOperation = _mapper.Map<OutComeMoneyOperation>(createMoneyOperationDto);

            await _outComeMoneyOperationRepository.Add(outComeMoneyOperation);

            return outComeMoneyOperation.Id;
        }

        public async Task<(CreatedDto outcomeId, CreatedDto incomeId)> CreateTransfer(CreateTransferOperationDto createTransferOperationDto)
        {
            var outComeMoneyOperation = _mapper.Map<OutComeMoneyOperation>(createTransferOperationDto);
            var incomeMoneyOperation = _mapper.Map<IncomeMoneyOperation>(createTransferOperationDto);

            outComeMoneyOperation.DateTime = DateTime.Now;
            incomeMoneyOperation.DateTime = DateTime.Now;

            await _outComeMoneyOperationRepository.Add(outComeMoneyOperation);
            await _incomeMoneyOperationRepository.Add(incomeMoneyOperation);

            return new(outComeMoneyOperation.Id, incomeMoneyOperation.Id);
        }

        public async Task<IncomeOutcomeDto> GetByPurse(long id)
        {
            var purse = await _purseRepository.GetById(id, p => p.IncomingOperations, p => p.OutComingOperations);

            var incomeOutcomeDto = _mapper.Map<IncomeOutcomeDto>(purse);

            return incomeOutcomeDto;
        }

        public async Task UpdateIncome(MoneyOperationDto moneyOperationDto)
        {
            var incomeMoneyOperation = await _incomeMoneyOperationRepository.GetById(moneyOperationDto.Id);

            if (incomeMoneyOperation == null)
            {
                throw new("Income not found");
            }

            _mapper.Map(moneyOperationDto, incomeMoneyOperation);

            await _incomeMoneyOperationRepository.Update(incomeMoneyOperation);
        }

        public async Task UpdateOutCome(MoneyOperationDto moneyOperationDto)
        {
            var outComeMoneyOperation = await _outComeMoneyOperationRepository.GetById(moneyOperationDto.Id);

            if (outComeMoneyOperation == null)
            {
                throw new("OutCome not found");
            }

            _mapper.Map(moneyOperationDto, outComeMoneyOperation);

            await _outComeMoneyOperationRepository.Update(outComeMoneyOperation);
        }
    }
}