namespace AutoParts.Controllers
{
    using AutoParts.Core.Constants;
    using AutoParts.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            ViewData[MessageConstant.SuccessMessage] = "Welcome";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            =>View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
}
