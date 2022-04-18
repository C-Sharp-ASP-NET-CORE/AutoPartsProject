namespace AutoParts.Infrastructure.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Deal
    {
        [Key]
        public int Id { get; set; }

        public int ContragentId { get; set; }

        [ForeignKey(nameof(ContragentId))]
        public Contragent Contragent { get; set; }

        public IList<DealSubject> DealSubjects { get; set; } = new List<DealSubject>();

    }
}