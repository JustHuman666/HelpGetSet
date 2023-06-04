using DAL.Context;
using DAL.Enteties;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace DAL.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly SiteContext _context;

        /// <summary>
        /// Constructor for creating a repository with given db context
        /// </summary>
        /// <param name="context">The instance of db context for the repository</param>
        public CountryRepository(SiteContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Country item)
        {
            await _context.Countries.AddAsync(item);
        }

        public void Delete(int id)
        {
            var country = _context.Countries.Find(id);
            if (country != null)
            {
                _context.Countries.Remove(country);
            }
        }

        public async Task<IQueryable<Country>> GetAllAsync()
        {
            var countries = await _context.Countries.ToListAsync();
            return countries.AsQueryable();
        }

        public async Task<IQueryable<Country>> GetAllWithDetailsAsync()
        {
            var countries = await _context.Countries
                .Include(country => country.CountryVersions)
                .Include(country => country.UsersFrom).ThenInclude(x => x.AppUser.UserProfile)
                .Include(country => country.UsersIn).ThenInclude(x => x.AppUser.UserProfile).ToListAsync();

            return countries.AsQueryable();
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            return await _context.Countries.FindAsync(id);
        }

        public async Task<Country> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Countries
                .Include(country => country.CountryVersions)
                .Include(country => country.UsersFrom).ThenInclude(x => x.OriginalCountry)
                .Include(country => country.UsersIn).ThenInclude(x => x.CurrentCountry)
                .FirstOrDefaultAsync(country => country.Id == id);
        }

        public async Task<Country> GetByNameAsync(string name)
        {
            return await _context.Countries
                .Include(country => country.CountryVersions)
                .Include(country => country.UsersFrom).ThenInclude(x => x.OriginalCountry)
                .Include(country => country.UsersIn).ThenInclude(x => x.CurrentCountry)
                .FirstOrDefaultAsync(country => country.Name == name);
        }

        public async Task<Country> GetByShortNameAsync(string shortName)
        {
            return await _context.Countries
                .Include(country => country.CountryVersions)
                .Include(country => country.UsersFrom).ThenInclude(x => x.OriginalCountry)
                .Include(country => country.UsersIn).ThenInclude(x => x.CurrentCountry)
                .FirstOrDefaultAsync(country => country.ShortName == shortName);
        }

        public void Update(Country item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
