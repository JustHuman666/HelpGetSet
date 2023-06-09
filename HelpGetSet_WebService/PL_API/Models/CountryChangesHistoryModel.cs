﻿namespace PL_API.Models
{
    public class CountryChangesHistoryModel
    {
        public int Id { get; set; }

        public DateTime ChangeTime { get; set; }

        public int CountryId { get; set; }

        public int AuthorId { get; set; }

        public string AuthorUsername { get; set; }

        public string RegistrationInfo { get; set; }

        public string EmploymentInfo { get; set; }

        public string TaxInfo { get; set; }

        public string InsuranceInfo { get; set; }

        public string SupportInfo { get; set; }

        public int ApprovesAmount { get; set; }

        public int DisApprovesAmount { get; set; }

        public virtual ICollection<int> UsersWhoChecked { get; set; }
    }
}
