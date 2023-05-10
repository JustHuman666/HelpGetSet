using EnumTypes;

namespace DAL.Enteties
{
    public class Migrant: BaseEntity
    {
        public bool IsOfficialRefugee { get; set; }

        public bool IsForcedMigrant { get; set; }

        public bool IsCommonMigrant { get; set; }

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
