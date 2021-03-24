using Microsoft.AspNetCore.Mvc;

namespace HelloTommy.Controllers
{
    [Route("Login")]
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}