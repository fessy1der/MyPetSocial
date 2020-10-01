using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyPetSocial.Controllers
{
    [Authorize]
    public class HomeController : ApiController
    {
        public ActionResult Get()
        {
            return Ok("Route Works Fine");
        }
    }
}
