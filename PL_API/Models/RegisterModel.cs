using System.ComponentModel.DataAnnotations;

namespace PL_API.Models
{
    public class RegisterModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "First name should consits of at least {2} characters", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Last name should consits of at least {2} characters", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "User name should consits of at least {2} characters", MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(50, ErrorMessage = "Password should consits of at least {2} characters", MinimumLength = 8)]
        public string Password { get; set; }
    }
}
