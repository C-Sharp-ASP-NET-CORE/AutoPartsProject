namespace AutoParts.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class DealSubject
    {
        [Key]
        public int Id { get; set; }

        public int DealId { get; set; }

        public int PartId { get; set; }

        public int PartCount { get; set; }

        [ForeignKey(nameof(DealId))]
        public Deal Deal { get; set; }

        [ForeignKey(nameof(PartId))]
        public Part Part { get; set; }
    }
}