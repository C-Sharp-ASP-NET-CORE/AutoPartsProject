namespace AutoParts.Core.Models.Parts
{
    public class PartDetailsServiceModel : PartServiceModel
    {
        public string Description { get; init; }

        public int CategoryId { get; init; }
        public string CategoryName { get; set; }

        public int DealerId { get; init; }

        public string DealerName { get; init; }

        public string UserId { get; init; }
    }
}
