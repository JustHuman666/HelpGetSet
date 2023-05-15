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

        Task<CountryChangesHistoryDto> GetLastCountryInfoByIdAsync(int id);

        Task<IEnumerable<CountryChangesHistoryDto>> GetAllCountryInfoVersionsByCountryIdAsync(int id);

        Task CreateCountryVersionAsync(CountryChangesHistoryDto item);

        Task ApproveVersionByIdAsync(int versionId, int userId);

        Task DisapproveVersionByIdAsync(int versionId, int userId);

        Task<bool> CheckIfUserAlreadyCheckedVersionAsync(int versionId, int userId);

        Task<bool> CheckIfUserApprovedOrDisapprovedAsync(int versionId, int userId);

        Task DeleteCountryInfoVersionAsync(int id);

        Task DeleteCountryInfoAsync(int id);
    }
}
