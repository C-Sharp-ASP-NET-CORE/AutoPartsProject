namespace AutoParts.Controllers
{
    using AutoParts.Core.Constants;
    using AutoParts.Core.Models.Dealers;
    using AutoParts.Infrastructure;
    using AutoParts.Infrastructure.Data;
    using AutoParts.Infrastructure.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    public class DealersController : Controller
    {
        private readonly AutoPartsDbContext data;

        public DealersController(AutoPartsDbContext data)
            => this.data = data;

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeDealerFormModel dealer)
        {
            var userId = this.User.Id();

            var userIdAlreadyDealer = this.data
                .Dealers
                .Any(d => d.UserId == userId);

            //if (userIdAlreadyDealer)
            //{
            //    ViewData[MessageConstant.ErrorMessage] = "You are already a dealer!";
            //    return BadRequest();
            //}

            if (!ModelState.IsValid)
            {
                return View(dealer);
            }

            var dealerData = new Dealer
            {
                Name = dealer.Name,
                PhoneNumber = dealer.PhoneNumber,
                UserId = userId
            };

            this.data.Dealers.Add(dealerData);
            this.data.SaveChanges();

            ViewData[MessageConstant.SuccessMessage] = "Thank you for becomming a dealer!";

            return RedirectToAction(nameof(PartsController.All), "Parts");
        }
    }
}