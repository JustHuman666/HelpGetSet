using EnumTypes;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Enteties
{
    public class Migrant: BaseEntity
    {
        public virtual UserProfile? User { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public bool IsOfficialRefugee { get; set; }

        public bool IsForcedMigrant { get; set; }

        public bool IsCommonMigrant { get; set; }

        public FamilyStatus FamilyStatus { get; set; }

        public int AmountOfChildren { get; set; }

        public bool IsEmployed { get; set; }

        public Housing Housing { get; set; }
    }
}
