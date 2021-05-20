using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DTOs.Invites;
using Models.DTOs.Misc;

namespace Services.ApiServices.Abstractions
{
    public interface IInviteService
    {
        Task<CreatedDto> CreateInvite(CreateInviteDto createInviteDto);

        Task<InviteWithIdDto> GetById(long id);

        Task AcceptInvite(long id);

        Task RejectInvite(long id);

        Task CancelInvite(long id);

        Task<ICollection<InviteWithIdDto>> GetIssued(long id);

        Task<ICollection<InviteWithIdDto>> GetReceived(long id);

        Task<ICollection<InviteWithIdDto>> GetByGroup(long id);
    }
}