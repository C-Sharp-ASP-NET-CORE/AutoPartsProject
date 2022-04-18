namespace AutoParts.Core.Models.Parts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllPartsQueryModel
    {
        public const int PartsPerPage = 8;
        public int CurrentPage { get; init; } = 1;
        public int TotalParts { get; set; }
        public string Brand { get; init; }
        public IEnumerable<string> Brands { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; init; }
        public PartsSorting Sorting { get; set; }
        public IEnumerable<PartListingViewModel> Parts { get; set; }
    }
}
