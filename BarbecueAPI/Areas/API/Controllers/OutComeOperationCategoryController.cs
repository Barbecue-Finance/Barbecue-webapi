using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarbecueAPI.Controllers;
using BarbecueAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Db;
using Models.Db.OperationCategories;
using Models.DTOs.OperationCategories;
using Models.DTOs.OperationCategories.OutCome;
using Services.ApiServices.Abstractions;

namespace BarbecueAPI.Areas.API.Controllers
{
    public class OutComeOperationCategoryController : BarbecueApiController
    {
        private readonly IOutComeOperationCategoryService _outComeOperationCategoryService;

        public OutComeOperationCategoryController(ITokenSessionService tokenSessionService,IOutComeOperationCategoryService outComeOperationCategoryService) : base(tokenSessionService)
        {
            _outComeOperationCategoryService = outComeOperationCategoryService;
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<OutComeOperationCategoryWithIdDto>> GetById([Id(typeof(OperationCategory))] long id)
        {
            try
            {
                var operationCategoryWithIdDto = await _outComeOperationCategoryService.GetById(id);

                return Ok(operationCategoryWithIdDto);
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<ICollection<OutComeOperationCategoryWithIdDto>>> GetAll()
        {
            try
            {
                var operationCategoryWithIdDtos = await _outComeOperationCategoryService.GetAll();

                return Ok(operationCategoryWithIdDtos);
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpPost]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<ICollection<OutComeOperationCategoryWithIdDto>>> Update([FromBody] UpdateOutComeOperationCategoryDto updateOperationCategoryDto)
        {
            try
            {
                await _outComeOperationCategoryService.Update(updateOperationCategoryDto);

                return Ok();
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpPost]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<ICollection<OutComeOperationCategoryWithIdDto>>> Create([FromBody] CreateOutComeOperationCategoryDto createOperationCategoryDto)
        {
            try
            {
                await _outComeOperationCategoryService.Create(createOperationCategoryDto);

                return Ok();
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpDelete]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<ICollection<OutComeOperationCategoryWithIdDto>>> Remove([Id(typeof(OperationCategory))] long id)
        {
            try
            {
                await _outComeOperationCategoryService.Remove(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<OutComeOperationCategoryWithIdDto>> GetByPurse([Id(typeof(Purse))] long id)
        {
            try
            {
                var outComeOperationCategoryWithIdDtos = await _outComeOperationCategoryService.GetByPurse(id);

                return Ok(outComeOperationCategoryWithIdDtos);
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }
    }
}