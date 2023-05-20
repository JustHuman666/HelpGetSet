namespace PL_API.Models
{
    public class PostModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreationTime { get; set; }

        public int AuthorId { get; set; }

        public int CountryId { get; set; }
    }
}
