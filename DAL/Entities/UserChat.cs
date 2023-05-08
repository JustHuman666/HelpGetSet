using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Enteties
{
    /// <summary>
    /// Class for representing of users chats relationships
    /// </summary>
    public class UserChat
    {
        public virtual UserProfile? User { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public virtual Chat? Chat { get; set; }
        [ForeignKey("ChatId")]
        public int ChatId { get; set; }

        public bool isAdmin { get; set; }

    }
}
