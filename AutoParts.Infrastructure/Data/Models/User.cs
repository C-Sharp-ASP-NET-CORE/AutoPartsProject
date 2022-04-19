namespace AutoParts.Infrastructure.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;
    public class User : IdentityUser
    {
        [MaxLength(UserFullNameMaxLength)]
        public string FullName { get; set; }
    }
}