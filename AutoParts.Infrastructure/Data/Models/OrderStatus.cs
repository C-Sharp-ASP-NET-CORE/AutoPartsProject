namespace AutoParts.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class OrderStatus
    {
        [Key]
        public int Id { get; init; }
        public string Name { get; set; }
    }
}
