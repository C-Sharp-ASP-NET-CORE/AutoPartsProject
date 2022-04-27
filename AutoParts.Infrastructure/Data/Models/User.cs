namespace AutoParts.Infrastructure.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;
    public class User : IdentityUser
    {
        [MaxLength(UserFullNameMaxLength)]
        public string FullName { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}