namespace AutoParts.Infrastructure.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static DataConstants;
    public class Rack
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(RackNumberMaxLength)]
        public string Number { get; set; }

        [Required]
        [StringLength(RackSectionMaxLength)]
        public string Section { get; set; }

        public bool IsInUse { get; set; } = true;

        public int PartsCount { get; set; } = 0;

        public int PartId { get; set; }

        [ForeignKey(nameof(PartId))]
        public Part Part { get; set; }
    }
}