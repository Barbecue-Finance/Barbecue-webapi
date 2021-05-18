using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.ApiServices.Abstractions;

namespace BarbecueAPI.Filters
{
    public class AuthTokenFilter : IAsyncActionFilter
    {
        private readonly ITokenSessionService _tokenSessionService;

        public AuthTokenFilter(ITokenSessionService tokenSessionService)
        {
            _tokenSessionService = tokenSessionService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
#if DEBUG
            Console.WriteLine("Skipping Auth-Token Check in DEBUG");
            await next.Invoke();
#else
            Console.WriteLine("Performing Auth-Token Check in RELEASE");
            if (!context.HttpContext.TryGetAuthToken(out var authToken))
            {
                context.Result = new BadRequestObjectResult(new ErrorDto(MessagesVerbatim.AuthTokenMissing));
                return;
            }

            var accountSession = await _tokenSessionService.GetByToken(authToken);
            if (accountSession == null)
            {
                context.Result = new UnauthorizedObjectResult(new ErrorDto(MessagesVerbatim.AuthTokenUnknown));
                return;
            }

            if (accountSession.EndDate > DateTime.Now)
            {
                await next.Invoke();
            }
            else
            {
                context.Result = new UnauthorizedObjectResult(new ErrorDto(MessagesVerbatim.AuthTokenExpired));
            }
#endif
        }
    }
}