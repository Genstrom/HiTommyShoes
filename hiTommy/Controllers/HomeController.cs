using Microsoft.AspNetCore.Mvc;

namespace hiTommy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Home()
        {
            return base.Content("<div>Home</div>", "text/html");
        }

        /* [Authorize]
         public IActionResult Secret()
         {
             return base.Content("<div>Secret</div>", "text/html");
         }
         
         [HttpGet]
         public IActionResult Authenticate()
         {
             var grandmaClaims = new List<Claim>()
             {
                 new Claim(ClaimTypes.Name, "Bob"),
                 new Claim("Grandma.Says", "Very Nice boy"),
                 new Claim(ClaimTypes.Email, "Bob@fmail.com")
             };
 
             var licenseClaims = new List<Claim>()
             {
                 new Claim("DrivingLicense", "A+"),
                 new Claim(ClaimTypes.Name, "Bob Banana")
             };
 
             var grandmaIdentity = new ClaimsIdentity(grandmaClaims, "Grandma Identity");
             var licenseIdentity = new ClaimsIdentity(licenseClaims, "Government Identity");
             var userPrincipal = new ClaimsPrincipal(new[] {grandmaIdentity, licenseIdentity});
             HttpContext.SignInAsync(userPrincipal);
             return Ok();
         }*/
    }
}