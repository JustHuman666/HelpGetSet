using DAL.Enteties;
using DAL.Entities;
using EnumTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.EntitiesDto
{
    public class UserProfileDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime Birthday { get; set; }

        public Gender Gender { get; set; }

        public virtual ICollection<int> CountryIds { get; set; }

        public virtual ICollection<int> PostIds { get; set; }

        public virtual ICollection<int> MadeCountryChangeIds { get; set; }

        public virtual ICollection<int> ChatIds { get; set; }

        public virtual ICollection<int> MessageIds { get; set; }

        public virtual ICollection<int> CountryVersionsChecked { get; set; }

        public virtual ICollection<int> MigrantsIds { get; set; }

        public virtual ICollection<int> VolunteersIds { get; set; }
    }
}
