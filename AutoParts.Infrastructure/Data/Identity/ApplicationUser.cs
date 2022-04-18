namespace AutoParts.Infrastructure.Data.Identity
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class ApplicationUser : IdentityUser
    {
        [StringLength(ApplicationUserFirstNameMaxLength)]
        public string? FirstName { get; set; }

        [StringLength(ApplicationUserLstNameMaxLength)]
        public string? LastName { get; set; }
    }
}
