using DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace DAL.Enteties
{
    public class Country: BaseEntity
    {
        [Required]
        [StringLength(30)]
        public string? Name { get; set; }

        [Required]
        public string? ShortName { get; set; }

        public virtual ICollection<UserProfile> UsersFrom { get; set; }
        public virtual ICollection<UserProfile> UsersIn { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<CountryChangesHistory> CountryVersions { get; set; }

        public Country()
        {
            UsersFrom ??= new HashSet<UserProfile>();
            UsersIn ??= new HashSet<UserProfile>();
            Posts ??= new HashSet<Post>();
            CountryVersions ??= new HashSet<CountryChangesHistory>();
        }
    }
}
