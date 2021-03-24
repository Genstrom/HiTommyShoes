using Microsoft.AspNetCore.Mvc;

namespace HelloTommy.Controllers
{
    public class AboutUsController : Controller
    {
        [Route("About")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}