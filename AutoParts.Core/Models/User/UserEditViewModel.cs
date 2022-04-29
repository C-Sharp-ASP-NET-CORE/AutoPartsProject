namespace AutoParts.Core.Models.User
{
    using System.ComponentModel.DataAnnotations;
    public class UserEditViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
    }
}
