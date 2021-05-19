using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using Models.Db;
using Models.DTOs.Invites;
using Models.DTOs.Misc;
using Services.ApiServices.Abstractions;

namespace Services.ApiServices.Implementations
{
    public class InviteService : IInviteService
    {
        private readonly IInviteRepository _inviteRepository;
        private readonly IGroupRepository _groupRepository;

        private readonly IMapper _mapper;

        public InviteService(IInviteRepository inviteRepository, IMapper mapper, IGroupRepository groupRepository)
        {
            _inviteRepository = inviteRepository;
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public async Task<CreatedDto> CreateInvite(CreateInviteDto createInviteDto)
        {
            var invite = _mapper.Map<Invite>(createInviteDto);

            var group = await _groupRepository.GetById(
                invite.GroupId,
                g => g.UsersRelation,
                g => g.Invites
            );

            if (group.UsersRelation.All(r => r.UserId != invite.IssuerId))
            {
                throw new("Not a member of group can't create an invite");
            }

            if (group.UsersRelation.Any(r => r.UserId == invite.RecipientId))
            {
                throw new("Recipient is already of this group!");
            }

            if (group.Invites.Any(i => i.RecipientId == invite.RecipientId && i.State == InviteState.Pending))
            {
                throw new("User is already invited!");
            }

            invite.State = InviteState.Pending;
            invite.IssuedAt = DateTime.Now;

            await _inviteRepository.Add(invite);

            return invite.Id;
        }

        public async Task AcceptInvite(long id)
        {
            var invite = await _inviteRepository.GetById(id);

            var group = await _groupRepository.GetById(
                invite.GroupId,
                g => g.UsersRelation
            );

            if (invite.IssuerId == invite.RecipientId)
            {
                throw new("You can't invite yourself");
            }

            if (group.UsersRelation.Any(r => r.UserId == invite.RecipientId))
            {
                throw new("Recipient is already in this group!");
            }

            if (invite.State != InviteState.Pending)
            {
                throw new("Can't accept invite, invalid state!");
            }

            group.UsersRelation.Add(new UserToGroup() {UserId = invite.RecipientId});
            await _groupRepository.Update(group);

            invite.State = InviteState.Accepted;

            await _inviteRepository.Update(invite);
        }

        public async Task RejectInvite(long id)
        {
            var invite = await _inviteRepository.GetById(id);

            invite.State = InviteState.Rejected;

            await _inviteRepository.Update(invite);
        }

        public async Task CancelInvite(long id)
        {
            var invite = await _inviteRepository.GetById(id);

            invite.State = InviteState.Canceled;

            await _inviteRepository.Update(invite);
        }

        public async Task<ICollection<InviteWithIdDto>> GetIssued(long id)
        {
            var invites = await _inviteRepository.GetMany(
                i => i.IssuerId == id,
                i => i.Recipient,
                i => i.Issuer,
                i => i.Group
            );

            var inviteWithIdDtos = _mapper.Map<ICollection<InviteWithIdDto>>(invites);

            return inviteWithIdDtos;
        }

        public async Task<ICollection<InviteWithIdDto>> GetReceived(long id)
        {
            var invites = await _inviteRepository.GetMany(
                i => i.IssuerId == id,
                i => i.Recipient,
                i => i.Issuer,
                i => i.Group
            );

            var inviteWithIdDtos = _mapper.Map<ICollection<InviteWithIdDto>>(invites);

            return inviteWithIdDtos;
        }
    }
}