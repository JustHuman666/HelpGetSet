using DAL.Enteties;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.EntitiesDto
{
    public class CountryDto
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
