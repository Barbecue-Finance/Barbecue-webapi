using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using Models.DTOs.Purses;
using Services.ApiServices.Abstractions;

namespace Services.ApiServices.Implementations
{
    public class PurseService : IPurseService
    {
        private readonly IPurseRepository _purseRepository;

        private readonly IMapper _mapper;

        public PurseService(IPurseRepository purseRepository, IMapper mapper)
        {
            _purseRepository = purseRepository;
            _mapper = mapper;
        }

        public async Task<PurseWithIdDto> GetById(long id)
        {
            var purse = await _purseRepository.GetById(
                id,
                p => p.IncomingOperations,
                p => p.OutComingOperations
            );

            var purseWithIdDto = _mapper.Map<PurseWithIdDto>(purse);

            return purseWithIdDto;
        }
    }
}