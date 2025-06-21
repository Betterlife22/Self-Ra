using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SELF_RA.Controllers
{
    [ApiController]
    [Route("/")]

    public class RootController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
