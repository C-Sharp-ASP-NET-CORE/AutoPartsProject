namespace AutoParts.Infrastructure.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;
    public class Contragent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ContragentCustomerNumberMaxLength,
            MinimumLength =ContragentCustomerNumberMinLength)]
        public string CustomerNumber { get; set; }

        [Required]
        [StringLength(ContragentNameMaxLength,
            MinimumLength =ContragentNameMinLength)]
        public string Name { get; set; }

        [StringLength(ContragentIdentifierMaxLength,
            MinimumLength =ContragentIdentifierMinLength)]
        public string? Identifier { get; set; }

        [StringLength(ContragentAddressMaxLength,
            MinimumLength =ContragentAddressMinLength)]
        public string? Address { get; set; }

        [StringLength(ContragentLoyaltyCardMaxLength,
            MinimumLength =ContragentLoyaltyCardMinLength)]
        public string? LoyaltyCard { get; set; }

        public IList<Deal> Deals { get; set; } = new List<Deal>();
    }
}