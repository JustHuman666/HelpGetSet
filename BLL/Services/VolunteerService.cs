using AutoMapper;
using BLL.EntitiesDto;
using BLL.Interfaces;
using BLL.Validation;
using DAL.Enteties;
using DAL.Interfaces.BaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class VolunteerService : IVolunteerService
    {
        private readonly IUnitOfWork _db;

        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for creating of service with given unit of work (database and operations) and automapper profile
        /// </summary>
        /// <param name="database">Instance of unit of work for this service</param>
        /// <param name="mapper">Instance of automapper profile</param>
        public VolunteerService(IUnitOfWork database, IMapper mapper)
        {
            _db = database;
            _mapper = mapper;
        }

        public async Task CreateVolunteerInfoAsync(VolunteerDto item)
        {
            if (item == null)
            {
                throw new HelpSiteException("Volunteer info cannot be null");
            }
            foreach (int userId in item.UserIds)
            {
                var user = await _db.Users.GetByIdAsync(userId);
                if ( user == null)
                {
                    throw new NotFoundException("There is no such user in the system");
                }
                if (user.UserProfile.MigrantId > 1 || user.UserProfile.VolunteerId > 1)
                {
                    throw new HelpSiteException("The user has already chosen type of profile");
                }
            }
            var volunteerToCreate = _mapper.Map<Volunteer>(item);
            await _db.Volunteers.CreateAsync(volunteerToCreate);
            await _db.SaveAsync();
        }

        public async Task DeleteVolunteerInfoAsync(int id)
        {
            var volunteerProfile = await GetVolunteerInfoByIdAsync(id);
            _db.Volunteers.Delete(id);
            await _db.SaveAsync();
        }

        public async Task<IEnumerable<VolunteerDto>> GetAllVolunteersAsync()
        {
            var volunteersProfiles = await _db.Volunteers.GetAllAsync();
            if (volunteersProfiles == null || volunteersProfiles.Count() == 0)
            {
                throw new NotFoundException("There is no volunteer profile");
            }
            return _mapper.Map<IEnumerable<VolunteerDto>>(volunteersProfiles);
        }

        public async Task<IEnumerable<VolunteerDto>> GetJustVolunteersAsync()
        {
            var allVolunteers = await GetAllVolunteersAsync();
            var notOrganisations = allVolunteers.Where(volunteer => !volunteer.IsOrganisation);
            if (notOrganisations == null || notOrganisations.Count() == 0)
            {
                throw new NotFoundException("There is no common volunteer, only organisations");
            }
            return notOrganisations;
        }

        public async Task<IEnumerable<VolunteerDto>> GetOrganisationsAsync()
        {
            var allVolunteers = await GetAllVolunteersAsync();
            var organisations = allVolunteers.Where(volunteer => volunteer.IsOrganisation);
            if (organisations == null || organisations.Count() == 0)
            {
                throw new NotFoundException("There is no organisations, only common volunteers");
            }
            return organisations;
        }

        public async Task<IEnumerable<VolunteerDto>> GetTranslatorsAsync()
        {
            var allVolunteers = await GetAllVolunteersAsync();
            var translators = allVolunteers.Where(volunteer => volunteer.IsATranslator);
            if (translators == null || translators.Count() == 0)
            {
                throw new NotFoundException("There is no translator");
            }
            return translators;
        }

        public async Task<VolunteerDto> GetVolunteerInfoByIdAsync(int id)
        {
            var volunteer = await _db.Volunteers.GetByIdAsync(id);
            if (volunteer == null)
            {
                throw new NotFoundException($"Version with the id: {id} does not exist");
            }
            return _mapper.Map<VolunteerDto>(volunteer);
        }

        public async Task<IEnumerable<VolunteerDto>> GetVolunteersForHousingAsync()
        {
            var allVolunteers = await GetAllVolunteersAsync();
            var volunteersWithAPlace = allVolunteers.Where(volunteer => volunteer.HasAPlace);
            if (volunteersWithAPlace == null || volunteersWithAPlace.Count() == 0)
            {
                throw new NotFoundException("There is no volunteer, who can help with housing");
            }
            return volunteersWithAPlace;
        }

        public async Task UpdateVolunteerInfoAsync(VolunteerDto item)
        {
            if (item == null)
            {
                throw new HelpSiteException("Volunteer info cannot be null");
            }
            var volunteerToUpdate = _mapper.Map<Volunteer>(item);
            await _db.Volunteers.CreateAsync(volunteerToUpdate);
            await _db.SaveAsync();
        }
    }
}
