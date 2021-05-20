using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using Models.Db.OperationCategories;
using Models.DTOs.Misc;
using Models.DTOs.OperationCategories.Income;
using Models.DTOs.OperationCategories.OutCome;
using Services.ApiServices.Abstractions;

namespace Services.ApiServices.Implementations
{
    public class IncomeOperationCategoryService : IIncomeOperationCategoryService
    {
        private readonly IIncomeOperationCategoryRepository _incomeOperationCategoryRepository;

        private readonly IMapper _mapper;

        public IncomeOperationCategoryService(IIncomeOperationCategoryRepository incomeOperationCategoryRepository, IMapper mapper)
        {
            _incomeOperationCategoryRepository = incomeOperationCategoryRepository;
            _mapper = mapper;
        }

        public async Task<IncomeOperationCategoryWithIdDto> GetById(long id)
        {
            var operationCategory = await _incomeOperationCategoryRepository.GetById(id);

            var operationCategoryWithIdDto = _mapper.Map<IncomeOperationCategoryWithIdDto>(operationCategory);
            return operationCategoryWithIdDto;
        }

        public async Task<ICollection<IncomeOperationCategoryWithIdDto>> GetAll()
        {
            var operationCategories = await _incomeOperationCategoryRepository.GetMany();

            var operationCategoryWithIdDtos = _mapper.Map<ICollection<IncomeOperationCategoryWithIdDto>>(operationCategories);

            return operationCategoryWithIdDtos;
        }

        public async Task Update(UpdateIncomeOperationCategoryDto updateDto)
        {
            var operationCategory = await _incomeOperationCategoryRepository.GetById(updateDto.Id);

            _mapper.Map(updateDto, operationCategory);

            await _incomeOperationCategoryRepository.Update(operationCategory);
        }

        public async Task<CreatedDto> Create(CreateIncomeOperationCategoryDto createDto)
        {
            var operationCategory = _mapper.Map<IncomeOperationCategory>(createDto);

            await _incomeOperationCategoryRepository.Add(operationCategory);

            return operationCategory.Id;
        }

        public async Task Remove(long id)
        {
            var operationCategory = await _incomeOperationCategoryRepository.GetById(id);
            await _incomeOperationCategoryRepository.Remove(operationCategory);
        }

        public async Task<ICollection<IncomeOperationCategoryWithIdDto>> GetByPurse(long id)
        {
            var operationCategories = await _incomeOperationCategoryRepository.GetMany(c => c.PurseId == id);

            var operationCategoryWithIdDtos = _mapper.Map<ICollection<IncomeOperationCategoryWithIdDto>>(operationCategories);

            return operationCategoryWithIdDtos;
        }
    }
}