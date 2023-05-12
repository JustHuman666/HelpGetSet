namespace PL_API.Models
{
    public class VolunteerModel
    {
        public bool IsOrganisation { get; set; }

        public bool HasAPlace { get; set; }

        public bool IsATranslator { get; set; }

        public virtual ICollection<int> UserIds { get; set; }
    }
}
