using EnumTypes;

namespace BLL.EntitiesDto
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime Birthday { get; set; }

        public Gender Gender { get; set; }

        public int OriginalCountryId { get; set; }

        public int CurrentCountryId { get; set; }

        public virtual UserProfileDto UserProfile { get; set; }
    }
}
