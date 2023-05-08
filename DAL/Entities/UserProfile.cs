using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EnumTypes;
using DAL.Entities;

namespace DAL.Enteties
{
    public class UserProfile
    {
        public virtual User AppUser { get; set; }
        [ForeignKey("UserId")]
        public int Id { get; set; }

        public virtual Volunteer Volunteer { get; set; }
        [ForeignKey("VolunteerId")]
        public int VolunteerId { get; set; }

        public virtual Migrant Migrant { get; set; }
        [ForeignKey("MigrantId")]
        public int MigrantId { get; set; }

        [Required]
        [StringLength(30)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string? LastName { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public virtual ICollection<UserCountry> Countries { get; set; }

        public virtual ICollection<UserChat> Chats { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<CountryChangesHistory> MadeCountryChanges { get; set; }

        public UserProfile()
        {
            Chats ??= new HashSet<UserChat>();
            Messages ??= new HashSet<Message>();
            Countries ??= new HashSet<UserCountry>();
            Posts ??= new HashSet<Post>();
            MadeCountryChanges ??= new HashSet<CountryChangesHistory>();
        }
    }
}
