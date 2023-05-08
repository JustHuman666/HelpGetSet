using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Enteties
{
    public class Volunteer: BaseEntity
    {
        public bool IsOrganisation { get; set; }

        public bool HasAPlace { get; set; }

        public bool IsATranslator { get; set; }

        public virtual ICollection<UserProfile> Users { get; set; }

        public Volunteer()
        {
            Users ??= new HashSet<UserProfile>();
        }
    }
}
