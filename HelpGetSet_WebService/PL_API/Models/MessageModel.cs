using System.ComponentModel.DataAnnotations;

namespace PL_API.Models
{
    public class MessageModel
    {
        [Required]
        [MinLength(1)]
        public string Text { get; set; }

        public int SenderId { get; set; }

        [Required]
        public int ChatId { get; set; }

        public DateTime SendingTime { get; set; }

        public int Id { get; set; }
    }
}
