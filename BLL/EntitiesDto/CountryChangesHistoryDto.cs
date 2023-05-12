using DAL.Enteties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.EntitiesDto
{
    public class CountryChangesHistoryDto
    {
        public int Id { get; set; }

        public int CountryId { get; set; }

        public int AuthorId { get; set; }

        public string AuthorUsername { get; set; }

        public DateTime ChangeTime { get; set; }

        public string RegistrationInfo { get; set; }

        public string EmploymentInfo { get; set; }

        public string TaxInfo { get; set; }

        public string InsuranceInfo { get; set; }

        public string SupportInfo { get; set; }

        public int ApprovesAmount { get; set; }

        public int DisApprovesAmount { get; set; }
    }
}
