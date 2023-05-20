using DAL.Enteties;
using DAL.Interfaces.BaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// An interface for user profiles repository
    /// </summary>
    public interface IUserProfileRepository : IDetailsRepository<UserProfile>
    {
        /// <summary>
        /// To get an instance of user profile by its id
        /// </summary>
        /// <param name="id">Id of profile that is found</param>
        /// <returns>An instance of found user profile</returns>
        Task<UserProfile> GetByIdAsync(int id);

        /// <summary>
        /// To get a collection of users from DB by first and last name
        /// </summary>
        /// <param name="firstname">Firstname of users</param>
        /// <param name="lastname">Lastname of users</param>
        /// <returns>A collection of users, which are found</returns>
        Task<IQueryable<UserProfile>> GetUsersByFirstAndLastNameAsync(string firstname, string lastname);
    }
}
