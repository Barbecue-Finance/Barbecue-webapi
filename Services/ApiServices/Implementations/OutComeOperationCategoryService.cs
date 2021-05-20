using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using Models.Db.OperationCategories;
using Models.DTOs.Misc;
using Models.DTOs.OperationCategories;
using Models.DTOs.OperationCategories.OutCome;
using Services.ApiServices.Abstractions;

namespace Services.ApiServices.Implementations
{
    public class OutComeOperationCategoryService : IOutComeOperationCategoryService
    {
        private readonly IOutComeOperationCategoryRepository _outComeOperationCategoryRepository;

        private readonly IMapper _mapper;

        public OutComeOperationCategoryService(IOutComeOperationCategoryRepository outComeOperationCategoryRepository, IMapper mapper)
        {
            _outComeOperationCategoryRepository = outComeOperationCategoryRepository;
            _mapper = mapper;
        }

        public async Task<OutComeOperationCategoryWithIdDto> GetById(long id)
        {
            var operationCategory = await _outComeOperationCategoryRepository.GetById(id);

            var operationCategoryWithIdDto = _mapper.Map<OutComeOperationCategoryWithIdDto>(operationCategory);
            return operationCategoryWithIdDto;
        }

        public async Task<ICollection<OutComeOperationCategoryWithIdDto>> GetAll()
        {
            var operationCategories = await _outComeOperationCategoryRepository.GetMany();

            var operationCategoryWithIdDtos = _mapper.Map<ICollection<OutComeOperationCategoryWithIdDto>>(operationCategories);

            return operationCategoryWithIdDtos;
        }

        public async Task Update(UpdateOutComeOperationCategoryDto updateDto)
        {
            var operationCategory = await _outComeOperationCategoryRepository.GetById(updateDto.Id);

            _mapper.Map(updateDto, operationCategory);

            await _outComeOperationCategoryRepository.Update(operationCategory);
        }

        public async Task<CreatedDto> Create(CreateOutComeOperationCategoryDto createDto)
        {
            var operationCategory = _mapper.Map<OutComeOperationCategory>(createDto);

            await _outComeOperationCategoryRepository.Add(operationCategory);

            return operationCategory.Id;
        }

        public async Task Remove(long id)
        {
            var operationCategory = await _outComeOperationCategoryRepository.GetById(id);
            await _outComeOperationCategoryRepository.Remove(operationCategory);
        }

        public async Task<ICollection<OutComeOperationCategoryWithIdDto>> GetByPurse(long id)
        {
            var operationCategories = await _outComeOperationCategoryRepository.GetMany(c => c.PurseId == id);

            var operationCategoryWithIdDtos = _mapper.Map<ICollection<OutComeOperationCategoryWithIdDto>>(operationCategories);

            return operationCategoryWithIdDtos;
        }
    }
}