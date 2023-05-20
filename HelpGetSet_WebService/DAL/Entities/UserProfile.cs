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

        public virtual ICollection<UserApprove> CountryVersionsChecked { get; set; }

        public virtual ICollection<Migrant> Migrants { get; set; }

        public virtual ICollection<Volunteer> Volunteers { get; set; }

        public UserProfile()
        {
            Chats ??= new HashSet<UserChat>();
            Messages ??= new HashSet<Message>();
            Countries ??= new HashSet<UserCountry>();
            Posts ??= new HashSet<Post>();
            MadeCountryChanges ??= new HashSet<CountryChangesHistory>();
            CountryVersionsChecked ??= new HashSet<UserApprove>();
            Migrants ??= new HashSet<Migrant>();
            Volunteers ??= new HashSet<Volunteer>();
        }
    }
}
