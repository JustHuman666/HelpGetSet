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
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _db;

        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for creating of service with given unit of work (database and operations) and automapper profile
        /// </summary>
        /// <param name="database">Instance of unit of work for this service</param>
        /// <param name="mapper">Instance of automapper profile</param>
        public CountryService(IUnitOfWork database, IMapper mapper)
        {
            _db = database;
            _mapper = mapper;
        }

        public async Task CreateCountryAsync(CountryDto item)
        {
            if (item == null)
            {
                throw new HelpSiteException("Country instance cannot be null");
            }
            if (string.IsNullOrEmpty(item.Name) || string.IsNullOrEmpty(item.ShortName))
            {
                throw new HelpSiteException("Country parameters cannot be empty");
            }
            if (item.ShortName.Contains(" "))
            {
                throw new HelpSiteException($"Country short name {item.ShortName} cannot contain empty spaces");
            }
            var existedWithName = await _db.Countries.GetByNameAsync(item.Name);
            if (existedWithName != null && item.Id != existedWithName.Id)
            {
                throw new HelpSiteException($"Country with the name {item.Name} already exists");
            }
            var existedWithShortName = await _db.Countries.GetByShortNameAsync(item.ShortName);
            if (existedWithShortName != null && item.Id != existedWithShortName.Id)
            {
                throw new HelpSiteException($"Country with the short name {item.ShortName} already exists");
            }
            var countryToCreate = _mapper.Map<Country>(item);
            await _db.Countries.CreateAsync(countryToCreate);
            await _db.SaveAsync();
        }

        public async Task DeleteCountryAsync(int id)
        {
            var country = await _db.Countries.GetByIdWithDetailsAsync(id);
            if (country == null)
            {
                throw new NotFoundException($"Country with id: {id} does not exist");
            }
            if (country.UsersFrom.Count() != 0 || country.UsersIn.Count() != 0)
            {
                throw new HelpSiteException($"There are already some user connected to the country: {country.Name}");
            }
            _db.Countries.Delete(id);
            await _db.SaveAsync();
        }

        public async Task<IEnumerable<CountryDto>> GetAllCountriesAsync()
        {
            var countries = await _db.Countries.GetAllAsync();
            if (countries == null || countries.Count() == 0)
            {
                throw new NotFoundException("There is no country");
            }
            return _mapper.Map<IEnumerable<CountryDto>>(countries);
        }

        public async Task<CountryDto> GetCountryByIdAsync(int id)
        {
            var country = await _db.Countries.GetByIdAsync(id);
            if (country == null)
            {
                throw new NotFoundException("There is no country with such id");
            }
            return _mapper.Map<CountryDto>(country);
        }

        public async Task<CountryDto> GetCountryByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new HelpSiteException("Country name cannot be empty");
            }
            var country = await _db.Countries.GetByNameAsync(name);
            if (country == null)
            {
                throw new NotFoundException("There is no country with such name");
            }
            return _mapper.Map<CountryDto>(country);
        }

        public async Task<CountryDto> GetCountryByShortNameAsync(string shortName)
        {
            if (string.IsNullOrEmpty(shortName))
            {
                throw new HelpSiteException("Country short name cannot be empty");
            }
            if (shortName.Contains(" "))
            {
                throw new HelpSiteException("Country's short name cannot contain empty spaces");
            }
            var country = await _db.Countries.GetByShortNameAsync(shortName);
            if (country == null)
            {
                throw new NotFoundException("There is no country with such short name");
            }
            return _mapper.Map<CountryDto>(country);
        }

        public async Task<IEnumerable<UserProfileDto>> GetUsersFromCountryByIdAsync(int id)
        {
            var country = await _db.Countries.GetByIdWithDetailsAsync(id);
            if (country == null || country.UsersFrom.Count() == 0)
            {
                throw new NotFoundException($"Country with id {id} does not exist or there is no user from country");
            }
            return _mapper.Map<IEnumerable<UserProfileDto>>(country.UsersFrom);
        }

        public async Task<IEnumerable<UserProfileDto>> GetUsersInCountryByIdAsync(int id)
        {
            var country = await _db.Countries.GetByIdWithDetailsAsync(id);
            if (country == null ||  country.UsersIn.Count() == 0)
            {
                throw new NotFoundException($"Country with id {id} does not exist or there is no user");
            }
            return _mapper.Map<IEnumerable<UserProfileDto>>(country.UsersIn);
        }

        public async Task UpdateCountryNamesAsync(CountryDto item)
        {
            if (item == null)
            {
                throw new HelpSiteException("Country instance cannot be null");
            }
            if (string.IsNullOrEmpty(item.Name) || string.IsNullOrEmpty(item.ShortName))
            {
                throw new HelpSiteException("Country parameters cannot be empty");
            }
            if (item.ShortName.Contains(" "))
            {
                throw new HelpSiteException($"Country short name {item.ShortName} cannot contain empty spaces");
            }
            var existedWithName = await _db.Countries.GetByNameAsync(item.Name);
            if (existedWithName != null && item.Id != existedWithName.Id)
            {
                throw new HelpSiteException($"Country with the name {item.Name} already exists");
            }
            var existedWithShortName = await _db.Countries.GetByShortNameAsync(item.ShortName);
            if (existedWithShortName != null && item.Id != existedWithShortName.Id)
            {
                throw new HelpSiteException($"Country with the short name {item.ShortName} already exists");
            }
            var countryToUpdate = _mapper.Map<Country>(item);
            _db.Countries.Update(countryToUpdate);
            await _db.SaveAsync();
        }
    }
}
