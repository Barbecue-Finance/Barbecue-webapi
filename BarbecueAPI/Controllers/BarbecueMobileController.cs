using BarbecueAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Misc;

namespace BarbecueAPI.Controllers
{
    // TODO: Remove this Route Attribute, it is only required for Swagger
    [Route("/m/[controller]/[action]/")]
    [TypeFilter(typeof(ValidateModel))]
    public abstract class BarbecueMobileController : Controller
    {
        [NonAction]
        protected ActionResult AkianaError(string error)
        {
            return BadRequest(new ErrorDto(error));
        }

        [NonAction]
        protected ActionResult AkianaMessage(string message)
        {
            return Ok(new MessageDto(message));
        }
    }
}