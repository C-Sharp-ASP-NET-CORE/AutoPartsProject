namespace AutoParts.Core.Services
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using AutoParts.Core.Contract;
    using AutoParts.Core.Models.Orders;
    using AutoParts.Infrastructure.Data;
    using AutoParts.Infrastructure.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class OrderService : IOrderService
    {
        private readonly AutoPartsDbContext data;
        private readonly IConfigurationProvider mapper;

        public OrderService(
            AutoPartsDbContext data,
            IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }
        public async Task<bool> CreateOrder(OrderServiceModel orderServiceModel)
        {
            var order = this.data.Orders.Select(o => new OrderServiceModel
            {
                IssuedOn = orderServiceModel.IssuedOn,
                Issuer = orderServiceModel.Issuer,
                IssuerId = orderServiceModel.IssuerId,
                Part = orderServiceModel.Part,
                PartId = orderServiceModel.PartId,
                Quantity = orderServiceModel.Quantity,
                Status = orderServiceModel.Status,
                StatusId = orderServiceModel.StatusId
            });

            this.data.Orders.Add((Order)order);
            int result = await this.data.SaveChangesAsync();

            return result > 0;
        }
    }
}