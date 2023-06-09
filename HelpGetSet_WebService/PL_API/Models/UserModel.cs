﻿using System.ComponentModel.DataAnnotations;

namespace PL_API.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "First name should consits of at least {2} characters", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Last name should consits of at least {2} characters", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "User name should consits of at least {2} characters", MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime Birthday { get; set; }

        public string Gender { get; set; }


        public int OriginalCountryId { get; set; }

        public int CurrentCountryId { get; set; }
    }
}
