using Microsoft.AspNetCore.Mvc;

namespace OTUS_SA_DZ12_WebAPI.Controllers
{
    public class OrdersDishesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
