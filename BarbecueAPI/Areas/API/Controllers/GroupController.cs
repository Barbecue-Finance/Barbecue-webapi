using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarbecueAPI.Controllers;
using BarbecueAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Db;
using Models.Db.Account;
using Models.DTOs.Groups;
using Models.DTOs.Misc;
using Services.ApiServices.Abstractions;

namespace BarbecueAPI.Areas.API.Controllers
{
    public class GroupController : BarbecueApiController
    {
        private readonly IGroupService _groupService;

        public GroupController(ITokenSessionService tokenSessionService, IGroupService groupService) : base(tokenSessionService)
        {
            _groupService = groupService;
        }

        [HttpPost]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<CreatedDto>> Create([FromBody] CreateGroupDto createGroupDto)
        {
            try
            {
                var createdDto = await _groupService.Create(createGroupDto);

                return Ok(createdDto);
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<ICollection<GroupWithIdDto>>> GetByUser([Id(typeof(User))] long id)
        {
            try
            {
                var groupWithIdDtos = await _groupService.GetByUser(id);

                return Ok(groupWithIdDtos);
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<GroupWithIdDto>> GetById([Id(typeof(Group))] long id)
        {
            try
            {
                var groupWithIdDto = await _groupService.GetById(id);

                return Ok(groupWithIdDto);
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<GroupWithIdDto>> Leave([Id(typeof(User))] long userId, [Id(typeof(Group))] long groupId)
        {
            try
            {
                await _groupService.Leave(userId, groupId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }
    }
}