using System;
using System.Threading.Tasks;
using BarbecueAPI.Controllers;
using BarbecueAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Db;
using Models.DTOs.Purses;
using Services.ApiServices.Abstractions;

namespace BarbecueAPI.Areas.API.Controllers
{
    public class PurseController : BarbecueApiController
    {
        private readonly IPurseService _purseService;
        
        public PurseController(ITokenSessionService tokenSessionService, IPurseService purseService) : base(tokenSessionService)
        {
            _purseService = purseService;
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<PurseWithIdDto>> GetById([Id(typeof(Purse))] long id)
        {
            try
            {
                var purseWithIdDto = await _purseService.GetById(id);

                return Ok(purseWithIdDto);
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }
    }
}