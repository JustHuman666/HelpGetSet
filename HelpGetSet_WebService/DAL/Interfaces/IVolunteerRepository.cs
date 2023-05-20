using DAL.Enteties;
using DAL.Interfaces.BaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IVolunteerRepository : IRepository<Volunteer>, IDetailsRepository<Volunteer>
    {
    }
}
