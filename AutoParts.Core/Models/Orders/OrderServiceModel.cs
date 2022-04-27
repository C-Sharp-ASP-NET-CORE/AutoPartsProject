namespace AutoParts.Core.Models.Orders
{
    using AutoMapper;
    using AutoParts.Core.Models.Parts;
    using System;

    public class OrderServiceModel
    {
        public DateTime IssuedOn { get; set; }
        public string PartId { get; set; }
        public PartServiceModel Part { get; set; }
        public int Quantity { get; set; }
        public string IssuerId { get; set; }
        public UserServiceModel Issuer { get; set; }
        public int StatusId { get; set; }
        public OrderStatusServiceModel Status { get; set; }
    }
}
