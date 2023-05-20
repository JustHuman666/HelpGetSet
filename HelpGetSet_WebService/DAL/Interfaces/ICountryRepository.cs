using DAL.Enteties;
using DAL.Interfaces.BaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// Repository interface for working with country
    /// </summary>
    public interface ICountryRepository : IRepository<Country>, IDetailsRepository<Country>
    {
        /// <summary>
        /// To get an instance of country from DB by its name
        /// </summary>
        /// <param name="name">Id of country that is found</param>
        /// <returns>An instance of found country</returns>
        Task<Country> GetByNameAsync(string name);

        /// <summary>
        /// To get an instance of country from DB by its short name
        /// </summary>
        /// <param name="shortName">Id of country that is found</param>
        /// <returns>An instance of found country</returns>
        Task<Country> GetByShortNameAsync(string shortName);
    }
}
