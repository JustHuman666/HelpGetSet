using DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace DAL.Enteties
{
    public class Country: BaseEntity
    {
        [Required]
        [StringLength(30)]
        public string? Name { get; set; }

        public virtual ICollection<UserCountry> Users { get; set; }

        public virtual ICollection<CountryChangesHistory> CountryVersions { get; set; }

        public Country()
        {
            Users ??= new HashSet<UserCountry>();
            CountryVersions ??= new HashSet<CountryChangesHistory>();
        }
    }
}
