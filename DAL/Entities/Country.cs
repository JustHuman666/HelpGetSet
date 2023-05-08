using System.ComponentModel.DataAnnotations;

namespace DAL.Enteties
{
    public class Country: BaseEntity
    {
        [Required]
        [StringLength(30)]
        public string? Name { get; set; }

        public string? RegistrationInfo { get; set; }

        public string? EmploymentInfo { get; set; }

        public string? TaxInfo { get; set; }

        public string? InsuranceInfo { get; set; }

        public string? SupportInfo { get; set; }

        public virtual ICollection<UserCountry> Users { get; set; }

        public Country()
        {
            Users ??= new HashSet<UserCountry>();
        }
    }
}
