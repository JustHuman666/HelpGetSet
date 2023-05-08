using DAL.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.BaseInterfaces
{
    public interface IVolunteerRepository: IRepository<Volunteer>, IDetailsRepository<Volunteer>
    {
    }
}
