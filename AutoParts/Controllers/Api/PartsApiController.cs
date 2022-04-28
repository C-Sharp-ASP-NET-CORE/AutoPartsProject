namespace AutoParts.Controllers.Api
{
    using AutoParts.Core.Contract;
    using AutoParts.Core.Models.Api;
    using AutoParts.Core.Models.Parts;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/parts")]
    public class PartsApiController : ControllerBase
    {
        private readonly IPartService parts;

        public PartsApiController(IPartService parts)
            => this.parts = parts;

        [HttpGet]
        public PartQueryServiceModel All([FromQuery] AllPartsApiRequestModel query)
            => this.parts.All(
                query.CarBrand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.PartsPerPage);
    }
}