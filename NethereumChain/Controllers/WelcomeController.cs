using Microsoft.AspNetCore.Mvc;

namespace NethereumChain.Controllers
{
    public class WelcomeController : Controller
    {
        [HttpGet]
        public IActionResult Get() => Ok("Hello.");
    }
}
