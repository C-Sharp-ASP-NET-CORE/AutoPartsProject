namespace AutoParts.Infrastructure.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;
    public class Dealer
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(DealerNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DealerPhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Part> Parts { get; init; } = new List<Part>();
    }
}