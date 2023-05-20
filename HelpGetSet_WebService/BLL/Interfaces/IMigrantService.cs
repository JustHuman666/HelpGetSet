using BLL.EntitiesDto;
using EnumTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMigrantService
    {
        Task<MigrantDto> GetMigrantInfoByIdAsync(int id);

        Task<MigrantDto> GetMigrantInfoByUserIdAsync(int id);

        Task<IEnumerable<MigrantDto>> GetAllMigrantsAsync();

        Task<IEnumerable<MigrantDto>> GetOfficialRefugeesAsync();

        Task<IEnumerable<MigrantDto>> GetForcedMigrantsAsync();

        Task<IEnumerable<MigrantDto>> GetCommonMigrantsAsync();

        Task<Housing> GetHousingTypeByMigrantIdAsync(int id);

        Task<FamilyStatus> GetFamilyStatusByMigrantIdAsync(int id);

        Task CreateMigrantInfoAsync(MigrantDto item);

        Task UpdateMigrantInfoAsync(MigrantDto item);

        Task DeleteMigrantInfoAsync(int id);

        Task<bool> CheckIfEmployedAsync(int id);
    }
}
