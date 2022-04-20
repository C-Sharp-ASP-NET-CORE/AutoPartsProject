namespace AutoParts.Controllers
{
    using AutoParts.Core.Contract;
    using AutoParts.Core.Models.Parts;
    using AutoParts.Infrastructure;
    using AutoParts.Infrastructure.Data;
    using AutoParts.Infrastructure.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class PartsController : Controller
    {
        private readonly IDealerService dealers;
        private readonly IPartService parts;

        public PartsController(
            IDealerService dealers,
             IPartService parts)
        {
            this.parts = parts;
            this.dealers = dealers;
        }


        [Authorize]
        public IActionResult Add()
        {
            if (!this.dealers.IsDealer(this.User.Id()))
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

            this.parts.Create(
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

            return RedirectToAction(nameof(All));
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

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.dealers.IsDealer(this.User.Id()))
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            var part = this.parts.Details(id);

            if (part.UserId != userId)
            {
                return Unauthorized();
            }

            return View(new PartFormModel
            {
                CategoryId = part.CategoryId,
                Manufacturer = part.Manufacturer,
                CarBrand = part.CarBrand,
                CarModel = part.CarModel,
                Price = part.Price,
                Description = part.Description,
                SerialNumber = part.SerialNumber,
                ImageUrl = part.ImageUrl,
                Year = part.Year,
                IsUsed = part.IsUsed,
                Categories = this.parts.AllPartCategories()
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, PartFormModel part)
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

            var partIsEdited = this.parts.Edit(
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
                dealerId);

            if (!partIsEdited)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }
    }
}
