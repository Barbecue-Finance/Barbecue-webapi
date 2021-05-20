using System;
using System.Threading.Tasks;
using BarbecueAPI.Controllers;
using BarbecueAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Db;
using Models.DTOs.Misc;
using Models.DTOs.MoneyOperations;
using Models.DTOs.MoneyOperations.Transfers;
using Models.DTOs.Purses;
using Services.ApiServices.Abstractions;

namespace BarbecueAPI.Areas.API.Controllers
{
    public class MoneyOperationController : BarbecueApiController
    {
        private readonly IMoneyOperationService _moneyOperationService;

        public MoneyOperationController(ITokenSessionService tokenSessionService, IMoneyOperationService moneyOperationService) : base(tokenSessionService)
        {
            _moneyOperationService = moneyOperationService;
        }

        [HttpPost]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<CreatedDto>> CreateIncome([FromBody] CreateMoneyOperationDto createMoneyOperationDto)
        {
            try
            {
                var createdDto = await _moneyOperationService.CreateIncome(createMoneyOperationDto);

                return createdDto;
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpPost]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<CreatedDto>> CreateOutCome([FromBody] CreateMoneyOperationDto createMoneyOperationDto)
        {
            try
            {
                var createdDto = await _moneyOperationService.CreateOutCome(createMoneyOperationDto);

                return createdDto;
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpPost]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<(CreatedDto outcomeId, CreatedDto incomeId)>> CreateTransfer([FromBody] CreateTransferOperationDto createTransferOperationDto)
        {
            try
            {
                var transfer = await _moneyOperationService.CreateTransfer(createTransferOperationDto);

                return transfer;
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<IncomeOutcomeDto>> GetByPurse([Id(typeof(Purse))] long id)
        {
            try
            {
                var incomeOutcomeDto = await _moneyOperationService.GetByPurse(id);

                return incomeOutcomeDto;
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpPost]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult> UpdateIncome([FromBody] OutComeMoneyOperationDto outComeMoneyOperationDto)
        {
            try
            {
                await _moneyOperationService.UpdateIncome(outComeMoneyOperationDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpPost]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult> UpdateOutCome([FromBody] OutComeMoneyOperationDto outComeMoneyOperationDto)
        {
            try
            {
                await _moneyOperationService.UpdateOutCome(outComeMoneyOperationDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }
    }
}