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

        Task<CountryDto> GetCountryByNameAsync(int name);

        Task<CountryDto> GetCountryByShortNameAsync(int shortName);

        Task<IEnumerable<CountryDto>> GetAllCountriesAsync();

        Task<IEnumerable<UserProfileDto>> GetUsersFromCountryByIdAsync(int id);

        Task<IEnumerable<UserProfileDto>> GetUsersInCountryByIdAsync(int id);

        Task CreateCountryAsync(CountryDto item);

        Task UpdateCountryNamesAsync(CountryDto item);

        Task ApproveNewVersionAsync(CountryDto item);

        Task DeleteCountryAsync(int id);
    }
}
