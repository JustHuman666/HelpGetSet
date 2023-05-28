using System.ComponentModel.DataAnnotations;

namespace PL_API.Models
{
    public class ChatModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Chat name should consits of at least {2} characters", MinimumLength = 2)]
        public string ChatName { get; set; }

        [Required]
        public virtual ICollection<int> UserIds { get; set; }
    }
}
