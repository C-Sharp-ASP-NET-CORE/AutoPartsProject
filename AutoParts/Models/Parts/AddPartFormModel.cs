namespace AutoParts.Models.Parts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class AddPartFormModel
    {
        [Display(Name = "Category")]
        public int CategoryId { get; init; }
        public IEnumerable<PartCategoryViewModel> Categories { get; set; }

        [Required]
        [StringLength(PartManufacturerMaxLength,
            MinimumLength = PartManufacturerMinLength)]
        public string Manufacturer { get; init; }

        [Required]
        [Display(Name = "Car Brand")]
        [StringLength(PartCarBrandMaxLength,
            MinimumLength = PartCarBrandMinLength)]
        public string CarBrand { get; init; }

        [Required]
        [Display(Name = "Car Model")]
        [StringLength(PartCarModelMaxLength,
           MinimumLength = PartCarModelMinLength)]
        public string CarModel { get; init; }

        [Range(PartPriceMinValue, PartPriceMaxValue)]
        public decimal Price { get; init; }

        [StringLength(PartDescriptionMaxLength,
            MinimumLength = PartDescriptionMinLength)]
        public string Description { get; init; }

        [Required]
        [Display(Name = "Image URL")]
        [Url]
        public string ImageUrl { get; init; }

        [Range(PartYearMinValue, PartYearMaxValue)]
        public int Year { get; init; }
    }
}
