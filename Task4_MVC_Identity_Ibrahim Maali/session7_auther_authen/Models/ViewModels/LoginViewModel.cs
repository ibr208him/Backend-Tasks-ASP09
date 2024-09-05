using System.ComponentModel.DataAnnotations;

namespace session7_auther_authen.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [MaxLength(50)]
        [Display(Name ="Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
