using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models.DTOs.Misc;

namespace BarbecueAPI.Filters
{
    public class ValidateModel : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(
                    new ErrorDto(
                        string.Join(
                            "\n", context.ModelState
                                .Where(e => e.Value.Errors.Count > 0)
                                .SelectMany(e => e.Value.Errors)
                                .Select(e => e.ErrorMessage)
                        )
                    )
                );
                return;
            }

            await next();
        }
    }
}