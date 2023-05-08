using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Enteties
{
    public class Message: BaseEntity
    {
        /// <summary>
        /// The content of this message
        /// </summary>
        [Required]
        [MinLength(1)]
        public string? Text { get; set; }

        /// <summary>
        /// Time of sending of this message
        /// </summary>
        [Required]
        public DateTime SendingTime { get; set; }

        /// <summary>
        /// Instance of user who send this message
        /// </summary>
        public virtual UserProfile? Sender { get; set; }
        [ForeignKey("SenderId")]
        public int SenderId { get; set; }

        /// <summary>
        /// Instance of chat where this message was sent
        /// </summary>
        public virtual Chat? UsersChat { get; set; }
        [ForeignKey("ChatId")]
        public int? ChatId { get; set; }

    }
}
