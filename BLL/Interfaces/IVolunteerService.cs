using BLL.EntitiesDto;
using EnumTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IVolunteerService
    {
        Task<VolunteerDto> GetVolunteerInfoByIdAsync(int id);

        Task<IEnumerable<VolunteerDto>> GetAllVolunteersAsync();

        Task<IEnumerable<VolunteerDto>> GetOrganisationsAsync();

        Task<IEnumerable<VolunteerDto>> GetJustVolunteersAsync();

        Task<IEnumerable<VolunteerDto>> GetTranslatorsAsync();

        Task<IEnumerable<VolunteerDto>> GetVolunteersForHousingAsync();

        Task CreateVolunteerInfoAsync(VolunteerDto item);

        Task UpdateVolunteerInfoAsync(VolunteerDto item);

        Task DeleteVolunteerInfoAsync(int id);

        Task CheckIfEmployedAsync(int id);
    }
}
