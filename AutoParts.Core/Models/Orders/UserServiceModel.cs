namespace AutoParts.Core.Models.Orders
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class UserServiceModel : IdentityUser
    {
        public string FullName { get; set; }
        public List<OrderStatusServiceModel> Orders { get; set; }
    }
}
