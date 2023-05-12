using DAL.Enteties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class CountryChangesHistory: BaseEntity
    {
        [Required]
        public Country? Country { get; set; }
        [ForeignKey("CountryId")]
        public int CountryId { get; set; }

        /// <summary>
        /// Instance of user who did this updating
        /// </summary>
        public virtual UserProfile? Author { get; set; }
        [ForeignKey("AuthorId")]
        public int AuthorId { get; set; }

        [Required]
        public string AuthorUsername { get; set; }

        [Required]
        public DateTime ChangeTime { get; set; }

        public string RegistrationInfo { get; set; }

        public string EmploymentInfo { get; set; }

        public string TaxInfo { get; set; }

        public string InsuranceInfo { get; set; }

        public string SupportInfo { get; set; }

        /// <summary>
        /// An amount of approvals for the information
        /// </summary>
        [Required]
        public int ApprovesAmount { get; set; }

        /// <summary>
        /// An amount of disapprovals for the information 
        /// </summary>
        [Required]
        public int DisApprovesAmount { get; set; }

        public virtual ICollection<UserApprove> UsersWhoChecked { get; set; }

        public CountryChangesHistory()
        {
            UsersWhoChecked ??= new HashSet<UserApprove>();
        }
    }
}
