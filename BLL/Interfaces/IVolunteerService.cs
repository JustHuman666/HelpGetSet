using BLL.EntitiesDto;

namespace BLL.Interfaces
{
    public interface IVolunteerService
    {
        Task<VolunteerDto> GetVolunteerInfoByIdAsync(int id);

        Task<VolunteerDto> GetVolunteerInfoByUserIdAsync(int id);

        Task<IEnumerable<VolunteerDto>> GetAllVolunteersAsync();

        Task<IEnumerable<VolunteerDto>> GetOrganisationsAsync();

        Task<IEnumerable<VolunteerDto>> GetJustVolunteersAsync();

        Task<IEnumerable<VolunteerDto>> GetTranslatorsAsync();

        Task<IEnumerable<VolunteerDto>> GetVolunteersForHousingAsync();

        Task CreateVolunteerInfoAsync(VolunteerDto item);

        Task UpdateVolunteerInfoAsync(VolunteerDto item);

        Task DeleteVolunteerInfoAsync(int id);
    }
}
