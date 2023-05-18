using AutoMapper;
using BLL.EntitiesDto;
using BLL.Interfaces;
using BLL.Validation;
using DAL.Enteties;
using DAL.Interfaces.BaseInterfaces;
using EnumTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MigrantService : IMigrantService
    {
        private readonly IUnitOfWork _db;

        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for creating of service with given unit of work (database and operations) and automapper profile
        /// </summary>
        /// <param name="database">Instance of unit of work for this service</param>
        /// <param name="mapper">Instance of automapper profile</param>
        public MigrantService(IUnitOfWork database, IMapper mapper)
        {
            _db = database;
            _mapper = mapper;
        }

        public async Task<bool> CheckIfEmployedAsync(int id)
        {
            var migrant = await GetMigrantInfoByIdAsync(id);
            return migrant.IsEmployed;
        }

        public async Task CreateMigrantInfoAsync(MigrantDto item)
        {
            if (item == null)
            {
                throw new HelpSiteException("Migrant info cannot be null");
            }
            var user = await _db.Users.GetByIdAsync(item.UserId);
            if (user == null)
            {
                throw new NotFoundException("There is no such user in the system");
            }
            var volunteers = await _db.Volunteers.GetAllAsync();
            var volunteer = volunteers.FirstOrDefault(volunteer => volunteer.UserId == item.UserId);
            var migrants = await _db.Migrants.GetAllAsync();
            var migrant = migrants.FirstOrDefault(migrant => migrant.UserId == item.UserId);
            if (volunteer != null || migrant != null)
            {
                throw new HelpSiteException($"There is already volunteer or migrant profile for the user");
            }

            var migrantToCreate = _mapper.Map<Migrant>(item);
            await _db.Migrants.CreateAsync(migrantToCreate);
            await _db.SaveAsync();
        }

        public async Task DeleteMigrantInfoAsync(int id)
        {
            var migrantProfile = await GetMigrantInfoByIdAsync(id);
            _db.Migrants.Delete(id);
            await _db.SaveAsync();
        }

        public async Task<IEnumerable<MigrantDto>> GetAllMigrantsAsync()
        {
            var migrantsProfiles = await _db.Migrants.GetAllAsync();
            if (migrantsProfiles == null || migrantsProfiles.Count() == 0)
            {
                throw new NotFoundException("There is no migrant profile");
            }
            return _mapper.Map<IEnumerable<MigrantDto>>(migrantsProfiles);
        }

        public async Task<IEnumerable<MigrantDto>> GetCommonMigrantsAsync()
        {
            var allMigrants = await GetAllMigrantsAsync();
            var commonMigrants = allMigrants.Where(migrant => migrant.IsCommonMigrant);
            if (commonMigrants == null || commonMigrants.Count() == 0)
            {
                throw new NotFoundException("There is no common migrant profile");
            }
            return commonMigrants;
        }

        public async Task<FamilyStatus> GetFamilyStatusByMigrantIdAsync(int id)
        {
            var migrant = await GetMigrantInfoByIdAsync(id);
            return migrant.FamilyStatus;
        }

        public async Task<IEnumerable<MigrantDto>> GetForcedMigrantsAsync()
        {
            var allMigrants = await GetAllMigrantsAsync();
            var forcedMigrants = allMigrants.Where(migrant => migrant.IsForcedMigrant);
            if (forcedMigrants == null || forcedMigrants.Count() == 0)
            {
                throw new NotFoundException("There is no forced migrant profile");
            }
            return forcedMigrants;
        }

        public async Task<Housing> GetHousingTypeByMigrantIdAsync(int id)
        {
            var migrant = await GetMigrantInfoByIdAsync(id);
            return migrant.Housing;
        }

        public async Task<MigrantDto> GetMigrantInfoByIdAsync(int id)
        {
            var migrant = await _db.Migrants.GetByIdAsync(id);
            if (migrant == null)
            {
                throw new NotFoundException($"Migrant with the id: {id} does not exist");
            }
            return _mapper.Map<MigrantDto>(migrant);
        }

        public async Task<MigrantDto> GetMigrantInfoByUserIdAsync(int id)
        {
            var user = await _db.Users.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"There is no user with an id: {id}");
            }
            var migrants = await _db.Migrants.GetAllAsync();
            var migrant = migrants.FirstOrDefault(migrant => migrant.UserId == id);
            if (migrant == null)
            {
                throw new NotFoundException($"There is no migrant profile for the user");
            }
            return _mapper.Map<MigrantDto>(migrant);
        }

        public async Task<IEnumerable<MigrantDto>> GetOfficialRefugeesAsync()
        {
            var allMigrants = await GetAllMigrantsAsync();
            var refugees = allMigrants.Where(migrant => migrant.IsOfficialRefugee);
            if (refugees == null || refugees.Count() == 0)
            {
                throw new NotFoundException("There is no refugee profile");
            }
            return refugees;
        }

        public async Task UpdateMigrantInfoAsync(MigrantDto item)
        {
            if (item == null)
            {
                throw new HelpSiteException("Migrant info cannot be null");
            }
            var migrantToUpdate = _mapper.Map<Migrant>(item);
            await _db.Migrants.CreateAsync(migrantToUpdate);
            await _db.SaveAsync();
        }
    }
}
