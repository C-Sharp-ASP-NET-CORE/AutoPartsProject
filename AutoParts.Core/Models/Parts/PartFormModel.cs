namespace AutoParts.Core.Models.Parts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Infrastructure.Data.DataConstants;
    public class PartFormModel
    {
        [Display(Name = "Category")]
        public int CategoryId { get; init; }
        public IEnumerable<PartCategoryServiceModel> Categories { get; set; }

        [Required]
        [StringLength(PartManufacturerMaxLength,
            MinimumLength = PartManufacturerMinLength)]
        public string Manufacturer { get; init; }

        [Required]
        [StringLength(PartCarBrandMaxLength,
            MinimumLength = PartCarBrandMinLength)]
        [Display(Name = "Car Brand")]
        public string CarBrand { get; init; }

        [Required]
        [StringLength(PartCarModelMaxLength,
           MinimumLength = PartCarModelMinLength)]
        [Display(Name = "Car Model")]
        public string CarModel { get; init; }

        [Range(PartPriceMinValue, PartPriceMaxValue)]
        public decimal Price { get; init; }

        [StringLength(PartDescriptionMaxLength,
            MinimumLength = PartDescriptionMinLength)]
        public string Description { get; init; }

        [Required]
        [StringLength(PartSerialNumberMaxLength,
            MinimumLength = PartSerialNumberMinLength)]
        public string SerialNumber { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }

        [Range(PartYearMinValue, PartYearMaxValue)]
        public int Year { get; init; }

        [Display(Name ="Is your part used (secondhand)?")]
        public bool IsUsed { get; set; }
    }
}
