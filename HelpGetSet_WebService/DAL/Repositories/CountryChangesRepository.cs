using DAL.Context;
using DAL.Enteties;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Interfaces.BaseInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CountryChangesRepository: ICountryHistoryRepository
    {
        private readonly SiteContext _context;

        /// <summary>
        /// Constructor for creating a repository with given db context
        /// </summary>
        /// <param name="context">The instance of db context for the repository</param>
        public CountryChangesRepository(SiteContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CountryChangesHistory item)
        {
            await _context.CountriesHistories.AddAsync(item);
        }

        public void Delete(int id)
        {
            var version = _context.CountriesHistories.Find(id);
            if (version != null)
            {
                _context.CountriesHistories.Remove(version);
            }
        }
        public async Task DeleteByCountryIdAsync(int id)
        {
            var versions = await _context.CountriesHistories.Where(version => version.CountryId == id).ToListAsync();
            if (versions.Count() != 0)
            {
                _context.CountriesHistories.RemoveRange(versions);
            }
        }

        public Task DisapproveIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<CountryChangesHistory>> GetAllAsync()
        {
            var versions = await _context.CountriesHistories.ToListAsync();
            return versions.AsQueryable();
        }

        public async Task<IQueryable<CountryChangesHistory>> GetAllWithDetailsAsync()
        {
            var versions = await _context.CountriesHistories
                .Include(version => version.Author)
                .Include(version => version.Country)
                .Include(version => version.UsersWhoChecked).ToListAsync();
            return versions.AsQueryable();
        }

        public async Task<IQueryable<CountryChangesHistory>> GetByCountryIdAsync(int id)
        {
            var versions = await _context.CountriesHistories
               .Where(version => version.CountryId == id)
               .Include(version => version.Author)
               .Include(version => version.Country)
               .Include(version => version.UsersWhoChecked)
               .ToListAsync();
            return versions.AsQueryable();
        }

        public async Task<CountryChangesHistory> GetByIdAsync(int id)
        {
            return await _context.CountriesHistories.FindAsync(id);
        }

        public async Task<CountryChangesHistory> GetByIdWithDetailsAsync(int id)
        {
            return await _context.CountriesHistories
                .Include(version => version.Author)
                .Include(version => version.Country)
                .Include(version => version.UsersWhoChecked)
                .FirstOrDefaultAsync(version => version.Id == id);
        }

        public void Update(CountryChangesHistory item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
