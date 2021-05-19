using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarbecueAPI.Controllers;
using BarbecueAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Db;
using Models.Db.Account;
using Models.DTOs.Invites;
using Models.DTOs.Misc;
using Services.ApiServices.Abstractions;

namespace BarbecueAPI.Areas.API.Controllers
{
    public class InviteController : BarbecueApiController
    {
        private readonly IInviteService _inviteService;

        public InviteController(ITokenSessionService tokenSessionService, IInviteService inviteService) : base(tokenSessionService)
        {
            _inviteService = inviteService;
        }

        [HttpPost]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<CreatedDto>> Create([FromBody] CreateInviteDto createInviteDto)
        {
            try
            {
                var createdDto = await _inviteService.CreateInvite(createInviteDto);

                return Ok(createdDto);
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult> Accept([Id(typeof(Invite))] long id)
        {
            try
            {
                await _inviteService.AcceptInvite(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult> Reject([Id(typeof(Invite))] long id)
        {
            try
            {
                await _inviteService.RejectInvite(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult> Cancel([Id(typeof(Invite))] long id)
        {
            try
            {
                await _inviteService.CancelInvite(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<ICollection<InviteWithIdDto>>> GetIssued([Id(typeof(User))] long id)
        {
            try
            {
                var inviteWithIdDtos = await _inviteService.GetIssued(id);

                return Ok(inviteWithIdDtos);
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<ICollection<InviteWithIdDto>>> GetReceived([Id(typeof(User))] long id)
        {
            try
            {
                var inviteWithIdDtos = await _inviteService.GetReceived(id);

                return Ok(inviteWithIdDtos);
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<ICollection<InviteWithIdDto>>> GetByGroup([Id(typeof(Group))] long id)
        {
            try
            {
                var inviteWithIdDtos = await _inviteService.GetByGroup(id);

                return Ok(inviteWithIdDtos);
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }
    }
}