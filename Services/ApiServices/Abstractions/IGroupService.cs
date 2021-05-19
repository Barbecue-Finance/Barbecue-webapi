using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DTOs.Groups;
using Models.DTOs.Misc;

namespace Services.ApiServices.Abstractions
{
    public interface IGroupService
    {
        Task<GroupWithIdDto> GetById(long id);
        
        Task<CreatedDto> Create(CreateGroupDto createGroupDto);

        Task<ICollection<GroupWithIdDto>> GetByUser(long id);

        Task Leave(long userId, long groupId);
    }
}