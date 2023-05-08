using DAL.Entities;
using DAL.Interfaces.BaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICountryHistoryRepository: IRepository<CountryChangesHistory>, IDetailsRepository<CountryChangesHistory>
    {
    }
}
