using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using Models.Db.Account;
using Models.DTOs.Misc;
using Models.DTOs.Users;
using Services.ApiServices.Abstractions;

namespace Services.ApiServices.Implementations
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        private IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserWithIdDto> GetById(long id)
        {
            var user = await _userRepository.GetById(id);

            var userWithIdDto = _mapper.Map<UserWithIdDto>(user);

            return userWithIdDto;
        }

        public async Task<ICollection<UserWithIdDto>> GetAll()
        {
            var users = await _userRepository.GetMany();

            var userWithIdDtos = _mapper.Map<ICollection<UserWithIdDto>>(users);

            return userWithIdDtos;
        }

        public async Task Update(UpdateUserDto updateDto)
        {
            var user = await _userRepository.GetById(updateDto.Id);

            _mapper.Map(updateDto, user);

            await _userRepository.Update(user);
        }

        public async Task<CreatedDto> Create(CreateUserDto createDto)
        {
            var user = _mapper.Map<User>(createDto);

            await _userRepository.Add(user);

            return user.Id;
        }

        public async Task Remove(long id)
        {
            var user = await _userRepository.GetById(id);

            await _userRepository.Remove(user);
        }

        public async Task<ICollection<UserWithIdDto>> GetByGroup(long id)
        {
            var users = await _userRepository.GetMany(
                u => u.GroupsRelation.Any(r => r.GroupId == id)
            );

            var userWithIdDto = _mapper.Map<ICollection<UserWithIdDto>>(users);

            return userWithIdDto;
        }
    }
}