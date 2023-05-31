using AutoMapper;
using BLL.EntitiesDto;
using BLL.Interfaces;
using BLL.Validation;
using DAL.Enteties;
using DAL.Entities;
using DAL.Interfaces.BaseInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    /// <summary>
    /// Service for working with countries information and all of the versions of changes etc.
    /// </summary>
    public class CountryChangesHistoryService: ICountryChangesHistoryService
    {
        private readonly IUnitOfWork _db;

        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for creating of service with given unit of work (database and operations) and automapper profile
        /// </summary>
        /// <param name="database">Instance of unit of work for this service</param>
        /// <param name="mapper">Instance of automapper profile</param>
        public CountryChangesHistoryService(IUnitOfWork database, IMapper mapper)
        {
            _db = database;
            _mapper = mapper;
        }

        public async Task ApproveVersionByIdAsync(int versionId, int userId)
        {
            if (await CheckIfUserAlreadyCheckedVersionAsync(versionId, userId))
            {
                throw new HelpSiteException($"User with the id: {userId} already checked the version with id: {versionId}");
            }
            var user = await _db.Users.GetByIdAsync(userId);
            var version = await _db.CountryVersions.GetByIdAsync(versionId);
            if (version == null)
            {
                throw new NotFoundException($"Version with the id: {versionId} does not exist");
            }
            version.ApprovesAmount += 1;
            var userApprove = new UserApprove()
            {
                Approved = true,
                CountryVersion = version,
                VersionId = versionId,
                DisApproved = false,
                UserId = userId,
                User = user.UserProfile
            };
            version.UsersWhoChecked.Add(userApprove);
            _db.CountryVersions.Update(version);
            await _db.SaveAsync();
        }

        public async Task<bool> CheckIfUserAlreadyCheckedVersionAsync(int versionId, int userId)
        {
            var user = await _db.Users.GetByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException($"User with id: {userId} does not exist");
            }
            var version = await _db.CountryVersions.GetByIdWithDetailsAsync(versionId);
            if (version == null)
            {
                throw new NotFoundException($"Version with the id: {versionId} does not exist");
            }
            return version.UsersWhoChecked.Any(userApprove => userApprove.UserId == userId);
        }

        public async Task<bool> CheckIfUserApprovedOrDisapprovedAsync(int versionId, int userId)
        {
            if(await CheckIfUserAlreadyCheckedVersionAsync(versionId, userId))
            {
                var version = await _db.CountryVersions.GetByIdAsync(versionId);
                return version.UsersWhoChecked.FirstOrDefault(approves => approves.UserId == userId).Approved;
            }
            else 
            {
                throw new HelpSiteException("User have not cheked this version yet");
            }
        }

        public async Task CreateCountryVersionAsync(CountryChangesHistoryDto item)
        {
            if (item == null)
            {
                throw new HelpSiteException("Country info version cannot be null");
            }
            if (string.IsNullOrEmpty(item.AuthorUsername)
                || string.IsNullOrEmpty(item.InsuranceInfo)
                || string.IsNullOrEmpty(item.TaxInfo)
                || string.IsNullOrEmpty(item.SupportInfo)
                || string.IsNullOrEmpty(item.EmploymentInfo))
            {
                throw new HelpSiteException("Users parameters cannot be empty");
            }
            var existedVersions = await _db.CountryVersions.GetAllAsync();
            if (!existedVersions.Any(version => version.InsuranceInfo == item.InsuranceInfo && version.TaxInfo == item.TaxInfo
                                    && version.SupportInfo == item.SupportInfo && version.EmploymentInfo == item.EmploymentInfo))
            {
                item.ApprovesAmount = 0;
                item.DisApprovesAmount = 0;
                item.ChangeTime = DateTime.Now;
                var versionToCreate = _mapper.Map<CountryChangesHistory>(item);
                await _db.CountryVersions.CreateAsync(versionToCreate);
                await _db.SaveAsync();
            }
        }

        public async Task DeleteCountryInfoAsync(int id)
        {
            var versions = await _db.CountryVersions.GetByCountryIdAsync(id);
            if (versions == null)
            {
                throw new NotFoundException($"There is no information about a country with the id: {id}");
            }
            await _db.CountryVersions.DeleteByCountryIdAsync(id);
            await _db.SaveAsync();
        }

        public async Task DeleteCountryInfoVersionAsync(int id)
        {
            var version = GetVersionByIdAsync(id);
            _db.CountryVersions.Delete(id);
            await _db.SaveAsync();
        }

        public async Task DisapproveVersionByIdAsync(int versionId, int userId)
        {
            if (await CheckIfUserAlreadyCheckedVersionAsync(versionId, userId))
            {
                throw new HelpSiteException($"User with the id: {userId} already checked the version with id: {versionId}");
            }
            var user = await _db.UsersProfiles.GetByIdAsync(userId);
            var version = await _db.CountryVersions.GetByIdAsync(versionId);
            if (version == null)
            {
                throw new NotFoundException($"Version with the id: {versionId} does not exist");
            }
            version.DisApprovesAmount += 1;
            version.UsersWhoChecked.Add(new UserApprove()
            {
                Approved = false,
                CountryVersion = version,
                VersionId = versionId,
                DisApproved = true,
                UserId = userId,
                User = user
            });
            _db.CountryVersions.Update(version);
            await _db.SaveAsync();
        }

        public async Task<IEnumerable<CountryChangesHistoryDto>> GetAllCountriesVersionsAsync()
        {
            var versions = await _db.CountryVersions.GetAllWithDetailsAsync();
            if (versions == null || versions.Count() == 0)
            {
                throw new NotFoundException("No countries info was found");
            }
            return _mapper.Map<IEnumerable<CountryChangesHistoryDto>>(versions);
        }

        public async Task<IEnumerable<CountryChangesHistoryDto>> GetAllCountryInfoVersionsByCountryIdAsync(int id)
        {
            var country = await _db.Countries.GetByIdAsync(id);
            if (country == null)
            {
                throw new NotFoundException($"No country with an ID: {id} does exist");
            }
            var countryVersions = await _db.CountryVersions.GetByCountryIdAsync(id);
            if (countryVersions == null || countryVersions.Count() == 0)
            {
                throw new NotFoundException($"No country info was found for country {country.Name}");
            }
            return _mapper.Map<IEnumerable<CountryChangesHistoryDto>>(countryVersions);
        }

        public async Task<CountryChangesHistoryDto> GetLastCountryInfoByIdAsync(int id)
        {
            var countryVersions = await GetAllCountryInfoVersionsByCountryIdAsync(id);
            var sortedVersions = countryVersions.OrderByDescending(version => version.ChangeTime).ToList();
            return sortedVersions.ElementAt(0);
        }

        public async Task<CountryChangesHistoryDto> GetVersionByIdAsync(int id)
        {
            var version = await _db.CountryVersions.GetByIdAsync(id);
            if (version == null)
            {
                throw new NotFoundException($"Version with the id: {id} does not exist");
            }
            return _mapper.Map<CountryChangesHistoryDto>(version);
        }
    }
}
