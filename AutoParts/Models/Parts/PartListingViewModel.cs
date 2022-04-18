namespace AutoParts.Models.Parts
{
    public class PartListingViewModel
    {
        public int Id { get; init; }
        public string Category { get; init; }
        public string CarBrand { get; init; }
        public string CarModel { get; init; }
        public decimal Price { get; init; }
        public int Year { get; init; }
        public string ImageUrl { get; init; }
    }
}
