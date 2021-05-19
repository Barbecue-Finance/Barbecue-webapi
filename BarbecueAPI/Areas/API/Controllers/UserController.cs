using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarbecueAPI.Controllers;
using BarbecueAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Db.Account;
using Models.DTOs.Misc;
using Models.DTOs.Requests;
using Models.DTOs.Responses;
using Models.DTOs.Users;
using Services.ApiServices.Abstractions;

namespace BarbecueAPI.Areas.API.Controllers
{
    public class UserController : BarbecueApiController
    {
        private readonly ITokenSessionService _tokenSessionService;
        private readonly IUserService _userService;

        public UserController(ITokenSessionService tokenSessionService, IUserService userService) : base(tokenSessionService)
        {
            _tokenSessionService = tokenSessionService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<LoginResultDto>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var loginResultDto = await _tokenSessionService.Login(loginDto);
                return loginResultDto;
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<MessageDto>> Logout()
        {
            var user = await GetRequestUser();
            try
            {
                await _tokenSessionService.Logout(user.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<UserWithIdDto>> GetById([Id(typeof(User))] long id)
        {
            try
            {
                return await _userService.GetById(id);
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<ICollection<UserWithIdDto>>> GetAll()
        {
            try
            {
                var withIdDtos = await _userService.GetAll();
                return Ok(withIdDtos);
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpPost]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult> Update([FromBody] UpdateUserDto updateDto)
        {
            try
            {
                await _userService.Update(updateDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreatedDto>> Create([FromBody] CreateUserDto createDto)
        {
            try
            {
                var createdDto = await _userService.Create(createDto);
                return createdDto;
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpDelete]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<CreatedDto>> Remove([Id(typeof(User))] long id)
        {
            try
            {
                await _userService.Remove(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }
    }
}