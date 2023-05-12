using EnumTypes;

namespace PL_API.Models
{
    public class MigrantModel
    {
        public int Id { get; set; }

        public bool IsOfficialRefugee { get; set; }

        public bool IsForcedMigrant { get; set; }

        public bool IsCommonMigrant { get; set; }

        public FamilyStatus FamilyStatus { get; set; }

        public int AmountOfChildren { get; set; }

        public bool IsEmployed { get; set; }

        public Housing Housing { get; set; }

        public virtual ICollection<int> UserIds { get; set; }
    }
}
