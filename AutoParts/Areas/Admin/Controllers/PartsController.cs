namespace AutoParts.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class PartsController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
