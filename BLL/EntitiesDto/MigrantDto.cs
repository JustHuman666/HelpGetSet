using DAL.Enteties;
using EnumTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.EntitiesDto
{
    public class MigrantDto
    {
        public int Id { get; set; }

        public bool IsOfficialRefugee { get; set; }

        public bool IsForcedMigrant { get; set; }

        public bool IsCommonMigrant { get; set; }

        public FamilyStatus FamilyStatus { get; set; }

        public int AmountOfChildren { get; set; }

        public bool IsEmployed { get; set; }

        public Housing Housing { get; set; }

        public int UserId { get; set; }
    }
}
