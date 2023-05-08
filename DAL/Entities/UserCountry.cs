using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Enteties
{
    public class UserCountry
    {
        [Required]
        public Country? Country { get; set; }
        [ForeignKey("CountryId")]
        public int CountryId { get; set; }

        [Required]
        public virtual UserProfile? User { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [Required]
        public bool OriginalCountry { get; set; }

        [Required]
        public bool CurrentCountry { get; set; }
    }
}
