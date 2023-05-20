using System.ComponentModel.DataAnnotations;

namespace PL_API.Models
{
    public class LoginModel
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
