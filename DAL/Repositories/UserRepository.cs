using DAL.Enteties;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    /// <summary>
    /// Class that represents an user repository
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Constructor for creating a repository with given db context
        /// </summary>
        /// <param name="context">The instance of db context for the repository</param>
        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddRoleAsync(User item, string role)
        {
            return await _userManager.AddToRoleAsync(item, role);
        }

        public async Task<IdentityResult> ChangePasswordAsync(User item, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(item, oldPassword, newPassword);
        }

        public async Task<bool> CheckPasswordAsync(User item, string password)
        {
            return await _userManager.CheckPasswordAsync(item, password);
        }

        public async Task<IdentityResult> CreateAsync(User item, string password)
        {
            return await _userManager.CreateAsync(item, password);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public IQueryable<User> GetAll()
        {
            var users = _userManager.Users.AsQueryable();
            return users;
        }

        public async Task<IEnumerable<string>> GetAllUserRoles(User item)
        {
            return await _userManager.GetRolesAsync(item);
        }

        public IQueryable<User> GetAllWithDetails()
        {
            var users = _userManager.Users
                .Include(x => x.UserProfile)
                .AsQueryable();
            return users;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<User> GetByIdWithDetailsAsync(int id)
        {
            return await _userManager.Users
                .Include(user => user.UserProfile)
                .FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<User> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _userManager.Users.FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber);
        }

        public async Task UpdateAsync(User item)
        {
            var user = await GetByIdWithDetailsAsync(item.Id);
            if (user != null)
            {
                user.UserName = item.UserName;
                user.NormalizedUserName = item.UserName.ToUpper();
                user.UserProfile.FirstName = item.UserProfile.FirstName;
                user.UserProfile.LastName = item.UserProfile.LastName;
                user.PhoneNumber = item.PhoneNumber;
                user.UserProfile.Birthday = item.UserProfile.Birthday;
                user.UserProfile.Countries = item.UserProfile.Countries;
                user.UserProfile.Gender = item.UserProfile.Gender;
            }
        }

    }
}
