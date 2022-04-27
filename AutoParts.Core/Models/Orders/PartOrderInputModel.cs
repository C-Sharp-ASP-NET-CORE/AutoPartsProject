namespace AutoParts.Core.Models.Orders
{
    public class PartOrderInputModel : OrderServiceModel
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
