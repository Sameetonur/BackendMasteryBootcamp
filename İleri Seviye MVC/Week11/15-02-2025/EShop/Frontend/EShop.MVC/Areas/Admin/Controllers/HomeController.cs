using Microsoft.AspNetCore.Mvc;

namespace EShop.MVC.Areas.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

    }
}
