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

        /// <summary>
        /// To get a collection of country changes from DB by country id
        /// </summary>
        /// <param name="id">Id of country, changes of which are being looked for</param>
        /// <returns></returns>
        Task<IQueryable<CountryChangesHistory>> GetByCountryIdAsync(int id);

        /// <summary>
        /// To delete country changes from DB by country id
        /// </summary>
        /// <param name="id">Id of country, changes of which are being deleted</param>
        Task DeleteByCountryIdAsync(int id);

    }
}
