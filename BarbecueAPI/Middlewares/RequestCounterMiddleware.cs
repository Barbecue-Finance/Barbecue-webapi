using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Services.Common.Abstractions;

namespace BarbecueAPI.Middlewares
{
    public class RequestCounterMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IRequestCounterService _requestCounterService;

        public RequestCounterMiddleware(RequestDelegate next, IRequestCounterService requestCounterService)
        {
            _next = next;
            _requestCounterService = requestCounterService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _requestCounterService.Notice(context.Request.Path);

            await _next.Invoke(context);
        }
    }
}