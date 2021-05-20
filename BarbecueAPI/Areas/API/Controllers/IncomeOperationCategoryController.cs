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
using Models.DTOs.OperationCategories.Income;
using Models.DTOs.OperationCategories.OutCome;
using Services.ApiServices.Abstractions;

namespace BarbecueAPI.Areas.API.Controllers
{
    public class IncomeOperationCategoryController : BarbecueApiController
    {
        private readonly IIncomeOperationCategoryService _incomeOperationCategoryService;

        public IncomeOperationCategoryController(ITokenSessionService tokenSessionService, IIncomeOperationCategoryService incomeOperationCategoryService) : base(tokenSessionService)
        {
            _incomeOperationCategoryService = incomeOperationCategoryService;
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<IncomeOperationCategoryWithIdDto>> GetById([Id(typeof(OperationCategory))] long id)
        {
            try
            {
                var operationCategoryWithIdDto = await _incomeOperationCategoryService.GetById(id);

                return Ok(operationCategoryWithIdDto);
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<ICollection<IncomeOperationCategoryWithIdDto>>> GetAll()
        {
            try
            {
                var operationCategoryWithIdDtos = await _incomeOperationCategoryService.GetAll();

                return Ok(operationCategoryWithIdDtos);
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpPost]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<ICollection<IncomeOperationCategoryWithIdDto>>> Update([FromBody] UpdateIncomeOperationCategoryDto updateOperationCategoryDto)
        {
            try
            {
                await _incomeOperationCategoryService.Update(updateOperationCategoryDto);

                return Ok();
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpPost]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<ICollection<IncomeOperationCategoryWithIdDto>>> Create([FromBody] CreateIncomeOperationCategoryDto createOperationCategoryDto)
        {
            try
            {
                await _incomeOperationCategoryService.Create(createOperationCategoryDto);

                return Ok();
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpDelete]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<ICollection<IncomeOperationCategoryWithIdDto>>> Remove([Id(typeof(OperationCategory))] long id)
        {
            try
            {
                await _incomeOperationCategoryService.Remove(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<IncomeOperationCategoryWithIdDto>> GetByPurse([Id(typeof(Purse))] long id)
        {
            try
            {
                var incomeOperationCategoryWithIdDtos = await _incomeOperationCategoryService.GetByPurse(id);

                return Ok(incomeOperationCategoryWithIdDtos);
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }
    }
}