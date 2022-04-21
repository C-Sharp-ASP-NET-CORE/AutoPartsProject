namespace AutoParts.Core.Models.Parts
{
    using AutoParts.Core.Contract;

    public class LatestPartServiceModel : IPartModel
    {
        public int Id { get; init; }

        public string CarBrand { get; init; }

        public string CarModel { get; init; }

        public string ImageUrl { get; init; }

        public int Year { get; init; }
        public string Category { get; init; }
    }
}