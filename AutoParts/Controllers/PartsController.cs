namespace AutoParts.Controllers
{
    using AutoMapper;
    using AutoParts.Core.Constants;
    using AutoParts.Core.Contract;
    using AutoParts.Core.Models.Parts;
    using AutoParts.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static Areas.Admin.AdminConstants;
    using static WebConstants;

    public class PartsController : BaseController
    {
        private readonly IDealerService dealers;
        private readonly IPartService parts;
        private readonly IMapper mapper;

        public PartsController(
            IDealerService dealers,
             IPartService parts,
             IMapper mapper)
        {
            this.parts = parts;
            this.dealers = dealers;
            this.mapper = mapper;
        }


        [Authorize]
        public IActionResult Add()
        {
            if (!this.dealers.IsDealer(this.User.Id()) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            return View(new PartFormModel
            {
                Categories = this.parts.AllPartCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(PartFormModel part)
        {
            var dealerId = this.dealers.IdByUser(User.Id());

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (!this.parts.CategoryExists(part.CategoryId))
            {
                this.ModelState.AddModelError(nameof(part.CategoryId), "Category does not exist");
            }

            if (!ModelState.IsValid)
            {
                part.Categories = this.parts.AllPartCategories();
                return View(part);
            }

            var partId = this.parts.Create(
                part.CategoryId,
                part.Manufacturer,
                part.CarBrand,
                part.CarModel,
                part.Price,
                part.Description,
                part.SerialNumber,
                part.ImageUrl,
                part.Year,
                part.IsUsed,
                dealerId);

            //ViewData[MessageConstant.SuccessMessage] = "Part Added and it's waiting for approval!";
            TempData[GlobalMessageKey] = "Part Added and it's waiting for approval!";

            return RedirectToAction(nameof(Details), new { id = partId, information = part.ToReadableURL() });
        }

        public IActionResult All([FromQuery] AllPartsQueryModel query)
        {
            var queryResult = this.parts.All(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllPartsQueryModel.PartsPerPage);

            var partBrands = this.parts.AllPartBrands();

            query.Brands = partBrands;
            query.TotalParts = queryResult.TotalParts;
            query.Parts = queryResult.Parts;

            return View(query);
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myParts = this.parts.ByUser(this.User.Id());

            return View(myParts);
        }

        public IActionResult Details(int id, string information)
        {
            var part = this.parts.Details(id);

            if (information != part.ToReadableURL())
            {
                return BadRequest();
            }

            return View(part);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {

            var userId = this.User.Id();

            if (!this.dealers.IsDealer(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            var part = this.parts.Details(id);

            if (part.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var partForm = this.mapper.Map<PartFormModel>(part);
            partForm.Categories = this.parts.AllPartCategories();

            return View(partForm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, PartFormModel part)
        {
            var dealerId = this.dealers.IdByUser(User.Id());

            if (dealerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (!this.parts.CategoryExists(part.CategoryId))
            {
                this.ModelState.AddModelError(nameof(part.CategoryId), "Category does not exist");
            }

            if (!ModelState.IsValid)
            {
                part.Categories = this.parts.AllPartCategories();
                return View(part);
            }

            if (!this.parts.IsByDealer(id, dealerId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            this.parts.Edit(
                id,
                part.CategoryId,
                part.Manufacturer,
                part.CarBrand,
                part.CarModel,
                part.Price,
                part.Description,
                part.SerialNumber,
                part.ImageUrl,
                part.Year,
                part.IsUsed,
                this.User.IsAdmin());

            //if (!partIsEdited)
            //{
            //    return BadRequest();
            //}

            TempData[GlobalMessageKey] = $"Part Edited{(this.User.IsAdmin() ? "!" : " and it's waiting for approval")}!";

            return RedirectToAction(nameof(Details), new { id, information = part.ToReadableURL() });
        }

        public IActionResult Delete(int id)
        {
            var partToDelete = this.parts.GetPartById(id);

            if (partToDelete == null)
            {
                ViewData[MessageConstant.ErrorMessage] = "Part does not exist!";
                return this.RedirectToAction("Invalid", "Home", new { area = "" });
            }

            return this.View(partToDelete);
        }

        [HttpPost]
        public IActionResult Delete(PartServiceModel part)
        {
            this.parts.Delete(part);
            return this.RedirectToAction("All", "Parts");
        }
    }
}
