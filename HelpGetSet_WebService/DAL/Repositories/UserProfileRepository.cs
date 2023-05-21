using DAL.Context;
using DAL.Enteties;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    /// <summary>
    /// Class that represents an user profile repository
    /// </summary>
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly SiteContext _context;

        /// <summary>
        /// Constructor for creating a repository with given db context
        /// </summary>
        /// <param name="context">The instance of db context for the repository</param>
        public UserProfileRepository(SiteContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<UserProfile>> GetAllWithDetailsAsync()
        {
            var userProfiles = await _context.UserProfiles
                .Include(user => user.AppUser)
                .Include(user => user.Countries)
                .Include(user => user.Posts)
                .Include(user => user.Messages)
                .Include(user => user.MadeCountryChanges)
                .Include(user => user.Chats)
                .Include(user => user.CountryVersionsChecked).ToListAsync();
            return userProfiles.AsQueryable();
        }

        public async Task<UserProfile> GetByIdAsync(int id)
        {
            return await _context.UserProfiles.FindAsync(id);
        }

        public async Task<UserProfile> GetByIdWithDetailsAsync(int id)
        {
            return await _context.UserProfiles
                .Include(user => user.AppUser)
                .Include(user => user.Countries)
                .Include(user => user.Posts)
                .Include(user => user.Messages)
                .Include(user => user.MadeCountryChanges)
                .Include(user => user.Chats)
                .Include(user => user.CountryVersionsChecked).FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<IQueryable<UserProfile>> GetUsersByFirstAndLastNameAsync(string firstname, string lastname)
        {
            var users = _context.UserProfiles
                .Where(user => user.FirstName == firstname && user.LastName == lastname)
                .Include(user => user.AppUser)
                .Include(user => user.Countries)
                .Include(user => user.Posts)
                .Include(user => user.Messages)
                .Include(user => user.MadeCountryChanges)
                .Include(user => user.Chats)
                .Include(user => user.CountryVersionsChecked)
                .AsQueryable();
            return users;
        }
    }
}
