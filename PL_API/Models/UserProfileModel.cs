using EnumTypes;

namespace PL_API.Models
{
    public class UserProfileModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public int VolunteerId { get; set; }

        public int MigrantId { get; set; }

        public DateTime Birthday { get; set; }

        public string Gender { get; set; }

        public virtual ICollection<int> CountryIds { get; set; }

        public virtual ICollection<int> PostIds { get; set; }

        public virtual ICollection<int> MadeCountryChangeIds { get; set; }

        public virtual ICollection<int> ChatIds { get; set; }

        public virtual ICollection<int> MessageIds { get; set; }
    }
}
