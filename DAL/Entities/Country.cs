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

        public virtual ICollection<UserCountry> Users { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<CountryChangesHistory> CountryVersions { get; set; }

        public Country()
        {
            Users ??= new HashSet<UserCountry>();
            Posts ??= new HashSet<Post>();
            CountryVersions ??= new HashSet<CountryChangesHistory>();
        }
    }
}
