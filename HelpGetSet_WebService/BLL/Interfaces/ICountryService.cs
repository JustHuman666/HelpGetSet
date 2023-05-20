using BLL.EntitiesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICountryService
    {
        Task<CountryDto> GetCountryByIdAsync(int id);

        Task<CountryDto> GetCountryByNameAsync(string name);

        Task<CountryDto> GetCountryByShortNameAsync(string shortName);

        Task<IEnumerable<CountryDto>> GetAllCountriesAsync();

        Task<IEnumerable<UserProfileDto>> GetUsersFromCountryByIdAsync(int id);

        Task<IEnumerable<UserProfileDto>> GetUsersInCountryByIdAsync(int id);

        Task CreateCountryAsync(CountryDto item);

        Task UpdateCountryNamesAsync(CountryDto item);

        Task DeleteCountryAsync(int id);
    }
}
