using Microsoft.AspNetCore.Mvc;

namespace OTUS_SA_DZ12_WebAPI.Controllers
{
    public class FeedbacksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
