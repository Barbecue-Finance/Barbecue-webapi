using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using Models.Db;
using Models.DTOs.Groups;
using Models.DTOs.Misc;
using Services.ApiServices.Abstractions;

namespace Services.ApiServices.Implementations
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;

        public GroupService(IGroupRepository groupRepository, IMapper mapper, IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<GroupWithIdDto> GetById(long id)
        {
            var group = await _groupRepository.GetById(id, g => g.Users);

            var groupWithIdDto = _mapper.Map<GroupWithIdDto>(group);

            return groupWithIdDto;
        }

        public async Task<CreatedDto> Create(CreateGroupDto createGroupDto)
        {
            var creator = await _userRepository.GetById(createGroupDto.CreatorId);

            var group = _mapper.Map<Group>(createGroupDto);

            group.UsersRelation = new List<UserToGroup>() {new() {UserId = creator.Id}};

            group.Purse = new Purse();

            await _groupRepository.Add(@group);

            return group.Id;
        }

        public async Task<ICollection<GroupWithIdDto>> GetByUser(long id)
        {
            var groups = await _groupRepository.GetMany(g => g.UsersRelation.Any(r => r.UserId == id), g => g.Users);

            var groupWithIdDtos = _mapper.Map<ICollection<GroupWithIdDto>>(groups);

            return groupWithIdDtos;
        }

        public async Task Leave(long userId, long groupId)
        {
            var group = await _groupRepository.GetById(groupId, g => g.UsersRelation);

            var userToGroup = group.UsersRelation.FirstOrDefault(r => r.UserId == userId);

            if (userToGroup == null)
            {
                throw new("Can't leave a group, not a member!");
            }

            group.UsersRelation.Remove(userToGroup);

            await _groupRepository.Update(@group);
        }
    }
}