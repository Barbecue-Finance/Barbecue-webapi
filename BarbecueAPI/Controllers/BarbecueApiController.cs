using System;
using System.Threading.Tasks;
using BarbecueAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Models.Db.Account;
using Models.DTOs.Misc;
using Services.ApiServices.Abstractions;

namespace BarbecueAPI.Controllers
{
    // TODO: Remove this Route Attribute, it is only required for Swagger
    [Route("/api/[controller]/[action]")]
    [TypeFilter(typeof(ValidateModel))]
    public abstract class BarbecueApiController : Controller
    {
        private readonly ITokenSessionService _tokenSessionService;

        protected BarbecueApiController(ITokenSessionService tokenSessionService)
        {
            _tokenSessionService = tokenSessionService;
        }

        [NonAction]
        protected ActionResult BarbecueError(string error)
        {
            return BadRequest(new ErrorDto(error));
        }

        [NonAction]
        protected ActionResult BarbecueMessage(string message)
        {
            return Ok(new MessageDto(message));
        }

        [NonAction]
        protected async Task<User> GetRequestUser()
        {
            var headers = ControllerContext.HttpContext.Request.Headers;
            if (headers.ContainsKey("auth-token"))
            {
                string authToken = headers["auth-token"];

                var userAccount = await _tokenSessionService.GetAccountByToken(authToken);

                if (userAccount != null)
                {
                    return userAccount;
                }
                else
                {
                    throw new ArgumentException($"{nameof(GetRequestUser)}() Was Called With No Session");
                }
            }
            else
            {
                throw new ArgumentException($"{nameof(GetRequestUser)}() Was Called With No auth-token Passed");
            }
        }
    }
}