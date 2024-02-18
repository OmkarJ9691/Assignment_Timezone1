using Microsoft.AspNetCore.Mvc;

namespace Assignment.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
