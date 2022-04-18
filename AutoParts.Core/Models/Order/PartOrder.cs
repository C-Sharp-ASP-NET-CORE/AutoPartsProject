namespace AutoParts.Core.Models.Order
{
    public class PartOrder
    {
        /// <summary>
        /// SerialNumber of the product
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Ordered products count
        /// </summary>
        public int Count { get; set; }
    }
}
