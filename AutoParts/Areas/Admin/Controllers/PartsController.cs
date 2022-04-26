namespace AutoParts.Areas.Admin.Controllers
{
    using AutoParts.Core.Contract;
    using Microsoft.AspNetCore.Mvc;

    public class PartsController : AdminController
    {
        private readonly IPartService parts;

        public PartsController(IPartService parts)
        {
            this.parts = parts;
        }
        public IActionResult All()
        {
            var parts = this.parts
                                .All(publicOnly: false)
                                .Parts;

            return View(parts);
        }

        public IActionResult ChangeVisibility(int id)
        { 
            this.parts.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
