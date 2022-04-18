namespace AutoParts.Core.Services
{
    using AutoParts.Core.Contracts;
    using AutoParts.Core.Models.Order;
    using AutoParts.Infrastructure.Data.Models;
    using AutoParts.Infrastructure.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class OrderService : IOrderService
    { 
        private readonly IApplicationDbRepository repo;

        public OrderService(IApplicationDbRepository _repo)
        {
            repo=_repo;
        }

        public async Task PlaceOrder(CustomerOrder order)
        {
            string customerNumber = order.CustomerNumber?.Trim() ?? string.Empty;
            var customer = await repo.All<Contragent>()
                .FirstOrDefaultAsync(c => c.CustomerNumber == customerNumber);

            if (customer == null)
            {
                throw new ArgumentException("Unknown customer");
            }

            var serialNumbers = order
                                    .Parts
                                    .Select(p => p.SerialNumber)
                                    .ToList();

            var products = await repo.All<Part>()
                .Where(p => serialNumbers.Contains(p.SerialNumber))
                .Include(p => p.Racks)
                .ToDictionaryAsync(k=>k.SerialNumber, v=>v.Racks.First().PartsCount);

            foreach (var part in order.Parts)
            {
                if (products.ContainsKey(part.SerialNumber) || 
                    part.Count > products[part.SerialNumber])
                {
                    throw new ArgumentException("Not enough quantity");
                }
            }

        }
    }
}
