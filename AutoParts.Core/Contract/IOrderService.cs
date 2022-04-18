namespace AutoParts.Core.Contracts
{
    using AutoParts.Core.Models.Order;
    using System.Threading.Tasks;
    public interface IOrderService
    {
        Task PlaceOrder(CustomerOrder order);
    }
}
