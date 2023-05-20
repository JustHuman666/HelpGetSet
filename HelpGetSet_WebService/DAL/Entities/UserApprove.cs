using DAL.Enteties;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    /// <summary>
    /// Class which represents relation between countries info versions and users approves
    /// </summary>
    public class UserApprove: BaseEntity
    {
        [Required]
        public virtual UserProfile? User { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [Required]
        public virtual CountryChangesHistory? CountryVersion { get; set; }
        [ForeignKey("VersionId")]
        public int VersionId { get; set; }

        public bool Approved { get; set; }

        public bool DisApproved { get; set; }
    }
}
