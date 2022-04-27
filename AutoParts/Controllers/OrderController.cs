namespace AutoParts.Controllers
{
    using AutoMapper;
    using AutoParts.Core.Contract;
    using AutoParts.Core.Models.Orders;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class OrderController : BaseController
    {
        private readonly IOrderService orders;
        private readonly IPartService parts;
        private readonly IConfigurationProvider mapper;

        public OrderController(
            IOrderService orders,
        IPartService parts,
             IMapper mapper)
        {
            this.parts = parts;
            this.orders = orders;
            this.mapper = mapper.ConfigurationProvider;
        }

        [HttpPost(Name = "Order")]
        public async Task<IActionResult> Order(PartOrderInputModel orderModel)
        {
            orderModel.IssuerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return Redirect("/");
        }
    }
}
