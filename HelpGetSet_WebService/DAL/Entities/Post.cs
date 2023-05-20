using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Enteties
{
    public class Post: BaseEntity
    {
        /// <summary>
        /// The content of this post
        /// </summary>
        [Required]
        [MinLength(1)]
        public string? Content { get; set; }

        /// <summary>
        /// Time of creating the post
        /// </summary>
        [Required]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Instance of user who created a post
        /// </summary>
        public virtual UserProfile? Author { get; set; }
        [ForeignKey("AuthorId")]
        public int AuthorId { get; set; }

        /// <summary>
        /// Instance of country which is related to a post
        /// </summary>
        public virtual Country? Country { get; set; }
        [ForeignKey("CountryId")]
        public int CountryId { get; set; }
    }
}
