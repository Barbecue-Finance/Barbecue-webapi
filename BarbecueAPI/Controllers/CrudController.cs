using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BarbecueAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Misc;
using Services.ApiServices;
using Services.ApiServices.Abstractions;

namespace BarbecueAPI.Controllers
{
    public abstract class CrudController<TBase, TWithIdDto, TCreateDto, TUpdateDto> : BarbecueApiController
    {
        private readonly ICrudService<TWithIdDto, TCreateDto, TUpdateDto> _service;
        private readonly Func<Type, object, Task<bool>> _existenceChecker;

        protected CrudController(ITokenSessionService tokenSessionService, ICrudService<TWithIdDto, TCreateDto, TUpdateDto> service, Func<Type, object, Task<bool>> existenceChecker) : base(tokenSessionService)
        {
            _service = service;
            _existenceChecker = existenceChecker;
        }

        [NonAction]
        private async Task EnsureExists(long id)
        {
            var exists = await _existenceChecker.Invoke(typeof(TBase), id);
            if (!exists)
            {
                throw new($"{typeof(TBase)} with Id({id}) not found");
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<TWithIdDto>> GetById(long id)
        {
            try
            {
                await EnsureExists(id);
                return await _service.GetById(id);
            }
            catch (Exception ex)
            {
                return AkianaError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<ICollection<TWithIdDto>>> GetAll()
        {
            try
            {
                var withIdDtos = await _service.GetAll();
                return Ok(withIdDtos);
            }
            catch (Exception ex)
            {
                return AkianaError(ex.Message);
            }
        }

        [HttpPost]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult> Update([FromBody] TUpdateDto updateDto)
        {
            try
            {
                await _service.Update(updateDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return AkianaError(ex.Message);
            }
        }

        [HttpPost]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<CreatedDto>> Create([FromBody] TCreateDto createDto)
        {
            try
            {
                var createdDto = await _service.Create(createDto);
                return createdDto;
            }
            catch (Exception ex)
            {
                return AkianaError(ex.Message);
            }
        }

        [HttpDelete]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<CreatedDto>> Remove(long id)
        {
            try
            {
                await EnsureExists(id);
                await _service.Remove(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return AkianaError(ex.Message);
            }
        }
    }
}