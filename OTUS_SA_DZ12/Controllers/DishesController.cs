using Microsoft.AspNetCore.Mvc;

namespace OTUS_SA_DZ12_WebAPI.Controllers
{
    public class DishesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
