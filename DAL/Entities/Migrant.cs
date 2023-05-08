using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Enteties
{
    public class Migrant: BaseEntity
    {
        public bool IsOfficialRefugee { get; set; }

        public bool IsMigrant { get; set; }

        public FamilyStatus FamilyStatus { get; set; }

        public int AmountOfChildren { get; set; }

        public bool IsEmployed { get; set; }

        public Housing Housing { get; set; }

        public virtual ICollection<UserProfile> Users { get; set; }

        public Migrant()
        {
            Users ??= new HashSet<UserProfile>();
        }

    }
}
