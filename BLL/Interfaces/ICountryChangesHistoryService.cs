using BLL.EntitiesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICountryChangesHistoryService
    {
        Task<CountryChangesHistoryDto> GetVersionByIdAsync(int id);

        Task<IEnumerable<CountryChangesHistoryDto>> GetAllCountriesVersionsAsync();

        Task<IEnumerable<CountryChangesHistoryDto>> GetLastCountryInfoByIdAsync(int id);

        Task<IEnumerable<CountryChangesHistoryDto>> GetAllCountryInfoVersionsByIdAsync(int id);

        Task CreateCountryVersionAsync(CountryChangesHistoryDto item);

        Task ApproveNewVersionAsync(CountryChangesHistoryDto item);

        Task DispproveNewVersionAsync(CountryChangesHistoryDto item);

        Task UpdateCountryVersionAsync(CountryChangesHistoryDto item);

        Task DeleteCountryInfoVersionAsync(int id);

        Task DeleteCountryInfoAsync(int id);
    }
}
