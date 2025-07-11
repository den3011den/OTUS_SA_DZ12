using Microsoft.AspNetCore.Mvc;

namespace OTUS_SA_DZ12_WebAPI.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
