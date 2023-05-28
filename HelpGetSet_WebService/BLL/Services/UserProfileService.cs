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
    /// <summary>
    /// Service for working getting information from user profiles
    /// </summary>
    public class UserProfileService : IUserProfileService
    {
        private readonly IUnitOfWork _db;

        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for creating of service with given unit of work (database and operations) and automapper profile
        /// </summary>
        /// <param name="database">Instance of unit of work for this service</param>
        /// <param name="mapper">Instance of automapper profile</param>
        public UserProfileService(IUnitOfWork database, IMapper mapper)
        {
            _db = database;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserProfileDto>> GetAllProfilesWithDetailsAsync()
        {
            var profiles = await _db.UsersProfiles.GetAllWithDetailsAsync();
            if (profiles == null || profiles.Count() == 0)
            {
                throw new NotFoundException("There is any user profile");
            }
            return _mapper.Map<IEnumerable<UserProfileDto>>(profiles);

        }

        public async Task<CountryDto> GetCurrentCountryByUserIdAsync(int id)
        {
            var profile = await _db.UsersProfiles.GetByIdWithDetailsAsync(id);
            if (profile == null)
            {
                throw new NotFoundException("User profile does not exist");
            }
            return _mapper.Map<CountryDto>(profile.CurrentCountry);
        }

        public async Task<CountryDto> GetOriginalCountryByUserIdAsync(int id)
        {
            var profile = await _db.UsersProfiles.GetByIdWithDetailsAsync(id);
            if (profile == null)
            {
                throw new NotFoundException("User profile does not exist");
            }
            return _mapper.Map<CountryDto>(profile.OriginalCountry);
        }

        public async Task<UserProfileDto> GetProfileByIdAsync(int id)
        {
            var profile = await _db.UsersProfiles.GetByIdAsync(id);
            if (profile == null)
            {
                throw new NotFoundException("User profile does not exist");
            }
            return _mapper.Map<UserProfileDto>(profile);
        }

        public async Task<UserProfileDto> GetProfileByIdWithDetailsAsync(int id)
        {
            var profile = await _db.UsersProfiles.GetByIdWithDetailsAsync(id);
            if (profile == null)
            {
                throw new NotFoundException("User profile does not exist");
            }
            return _mapper.Map<UserProfileDto>(profile);
        }

    }
}
