﻿using DAL.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.EntitiesDto
{
    public class VolunteerDto
    {
        public int Id { get; set; }

        public bool IsOrganisation { get; set; }

        public bool HasAPlace { get; set; }

        public bool IsATranslator { get; set; }

        public int UserId { get; set; }
    }
}
