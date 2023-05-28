namespace PL_API.Models
{
    public class CountryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public virtual ICollection<int> UsersFromIds { get; set; }

        public virtual ICollection<int> UsersInIds { get; set; }

        public virtual ICollection<int> PostIds { get; set; }

        public virtual ICollection<int> CountryVersionIds { get; set; }
    }
}
