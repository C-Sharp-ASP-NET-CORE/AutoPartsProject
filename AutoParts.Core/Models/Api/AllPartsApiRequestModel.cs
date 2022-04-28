namespace AutoParts.Core.Models.Api
{
    using AutoParts.Core.Models.Parts;

    public class AllPartsApiRequestModel
    {
        public string CarBrand { get; init; }
        public string SearchTerm { get; init; }
        public PartsSorting Sorting { get; init; }
        public int CurrentPage { get; init; } = 1;
        public int PartsPerPage { get; init; } = 6;
    }
}
