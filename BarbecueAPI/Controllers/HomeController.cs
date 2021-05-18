using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BarbecueAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET
        [HttpGet]
        public IActionResult Test(string data)
        {
            return Ok(data);
        }
    }
}