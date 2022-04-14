namespace AutoParts.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;
    public class Part
    {
        [Key]
        public int Id { get; init; }
        public int CategoryId { get; set; }
        public Category Category { get; init; }
        [Required]
        [StringLength(PartManufacturerMaxLength)]
        public string Manufacturer { get; set; }
        [Required]
        [StringLength(PartCarBrandMaxLength)]
        public string CarBrand { get; set; }
        [Required]
        [StringLength(PartCarModelMaxLength)]
        public string CarModel { get; set; }
        [Range(PartPriceMinValue,PartPriceMaxValue)]
        public decimal Price { get; set; }
        [StringLength(PartDescriptionMaxLength)]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Range(PartYearMinValue,PartYearMaxValue)]
        public int Year { get; set; }
    }
}
