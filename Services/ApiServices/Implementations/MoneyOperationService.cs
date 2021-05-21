using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using Models.Db;
using Models.Db.MoneyOperations;
using Models.Db.OperationCategories;
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
        private readonly IIncomeOperationCategoryRepository _incomeOperationCategoryRepository;
        private readonly IOutComeOperationCategoryRepository _outComeOperationCategoryRepository;

        private readonly IMapper _mapper;

        public MoneyOperationService(IIncomeMoneyOperationRepository incomeMoneyOperationRepository, IOutComeMoneyOperationRepository outComeMoneyOperationRepository, IMapper mapper, IPurseRepository purseRepository, IIncomeOperationCategoryRepository incomeOperationCategoryRepository, IOutComeOperationCategoryRepository outComeOperationCategoryRepository)
        {
            _incomeMoneyOperationRepository = incomeMoneyOperationRepository;
            _outComeMoneyOperationRepository = outComeMoneyOperationRepository;
            _mapper = mapper;
            _purseRepository = purseRepository;
            _incomeOperationCategoryRepository = incomeOperationCategoryRepository;
            _outComeOperationCategoryRepository = outComeOperationCategoryRepository;
        }

        public async Task<CreatedDto> CreateIncome(CreateMoneyOperationDto createMoneyOperationDto)
        {
            var incomeMoneyOperation = _mapper.Map<IncomeMoneyOperation>(createMoneyOperationDto);

            incomeMoneyOperation.DateTime = DateTime.Now;

            var operationCategory = await _incomeOperationCategoryRepository.GetOne(c =>
                c.Title == createMoneyOperationDto.OperationCategoryTitle && c.PurseId == createMoneyOperationDto.PurseId
            );

            if (operationCategory == null)
            {
                var category = new IncomeOperationCategory()
                {
                    Title = createMoneyOperationDto.OperationCategoryTitle,
                    PurseId = createMoneyOperationDto.PurseId
                };
                await _incomeOperationCategoryRepository.Add(category);
                incomeMoneyOperation.IncomeOperationCategory = category;
            }
            else
            {
                incomeMoneyOperation.IncomeOperationCategoryId = operationCategory.Id;
            }

            await _incomeMoneyOperationRepository.Add(incomeMoneyOperation);

            return incomeMoneyOperation.Id;
        }

        public async Task<CreatedDto> CreateOutCome(CreateMoneyOperationDto createMoneyOperationDto)
        {
            var outComeMoneyOperation = _mapper.Map<OutComeMoneyOperation>(createMoneyOperationDto);

            outComeMoneyOperation.DateTime = DateTime.Now;

            var operationCategory = await _incomeOperationCategoryRepository.GetOne(c => c.Title == createMoneyOperationDto.OperationCategoryTitle && c.PurseId == createMoneyOperationDto.PurseId);

            if (operationCategory == null)
            {
                var category = new OutComeOperationCategory()
                {
                    Title = createMoneyOperationDto.OperationCategoryTitle,
                    PurseId = createMoneyOperationDto.PurseId
                };
                await _outComeOperationCategoryRepository.Add(category);
                outComeMoneyOperation.OutComeOperationCategory = category;
            }
            else
            {
                outComeMoneyOperation.OutComeOperationCategoryId = operationCategory.Id;
            }

            await _outComeMoneyOperationRepository.Add(outComeMoneyOperation);

            return outComeMoneyOperation.Id;
        }

        public async Task<(CreatedDto outcomeId, CreatedDto incomeId)> CreateTransfer(CreateTransferOperationDto createTransferOperationDto)
        {
            var outComeMoneyOperation = _mapper.Map<OutComeMoneyOperation>(createTransferOperationDto);
            var incomeMoneyOperation = _mapper.Map<IncomeMoneyOperation>(createTransferOperationDto);

            var outComeOperationCategory = await _outComeOperationCategoryRepository.GetOne(c => c.PurseId == outComeMoneyOperation.PurseId && c.Title == "Трансферы");
            var incomeOperationCategory = await _incomeOperationCategoryRepository.GetOne(c => c.PurseId == incomeMoneyOperation.PurseId && c.Title == "Трансферы");

            outComeMoneyOperation.DateTime = DateTime.Now;
            outComeMoneyOperation.OutComeOperationCategoryId = outComeOperationCategory.Id;

            incomeMoneyOperation.DateTime = DateTime.Now;
            incomeMoneyOperation.IncomeOperationCategoryId = incomeOperationCategory.Id;

            await _outComeMoneyOperationRepository.Add(outComeMoneyOperation);
            await _incomeMoneyOperationRepository.Add(incomeMoneyOperation);

            return new(outComeMoneyOperation.Id, incomeMoneyOperation.Id);
        }

        public async Task<IncomeOutcomeDto> GetByPurse(long id)
        {
            var purse = await _purseRepository.GetById(id,
                p => p.IncomingOperations,
                p => p.OutComingOperations
            );

            await _incomeMoneyOperationRepository.GetMany(i => i.PurseId == id,
                i => i.User,
                i => i.IncomeOperationCategory
            );

            await _outComeMoneyOperationRepository.GetMany(i => i.PurseId == id,
                i => i.User,
                i => i.OutComeOperationCategory
            );

            var incomeOutcomeDto = _mapper.Map<IncomeOutcomeDto>(purse);

            return incomeOutcomeDto;
        }

        public async Task UpdateIncome(OutComeMoneyOperationDto outComeMoneyOperationDto)
        {
            var incomeMoneyOperation = await _incomeMoneyOperationRepository.GetById(outComeMoneyOperationDto.Id);

            if (incomeMoneyOperation == null)
            {
                throw new("Income not found");
            }

            _mapper.Map(outComeMoneyOperationDto, incomeMoneyOperation);

            await _incomeMoneyOperationRepository.Update(incomeMoneyOperation);
        }

        public async Task UpdateOutCome(OutComeMoneyOperationDto outComeMoneyOperationDto)
        {
            var outComeMoneyOperation = await _outComeMoneyOperationRepository.GetById(outComeMoneyOperationDto.Id);

            if (outComeMoneyOperation == null)
            {
                throw new("OutCome not found");
            }

            _mapper.Map(outComeMoneyOperationDto, outComeMoneyOperation);

            await _outComeMoneyOperationRepository.Update(outComeMoneyOperation);
        }
    }
}