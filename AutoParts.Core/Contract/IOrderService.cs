namespace AutoParts.Core.Contract
{
    using AutoParts.Core.Models.Orders;
    using System.Threading.Tasks;

    public interface IOrderService
    {
        Task<bool> CreateOrder(OrderServiceModel orderServiceModel);
    }
}
