namespace AutoParts.Infrastructure.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        [Key]
        public int Id { get; init; }
        public DateTime IssuedOn { get; set; }
        public string PartId { get; set; }
        public Part Part { get; set; }
        public int Quantity { get; set; }
        public string IssuerId { get; set; }
        public User Issuer { get; set; }
        public int StatusId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
