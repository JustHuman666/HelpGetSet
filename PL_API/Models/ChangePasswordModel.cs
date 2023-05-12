using System.ComponentModel.DataAnnotations;

namespace PL_API.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(50, ErrorMessage = "Password should consits ofat least {2} characters", MinimumLength = 8)]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(50, ErrorMessage = "Password should consits of at least {2} characters", MinimumLength = 8)]
        public string NewPassword { get; set; }
    }
}
