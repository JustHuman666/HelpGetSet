using BLL.EntitiesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    /// <summary>
    /// Service interface for working with users profiles
    /// </summary>
    public interface IUserProfileService
    {
        /// <summary>
        /// To get an instance of user profile by its id
        /// </summary>
        /// <param name="id">Id of profile that is found</param>
        /// <returns>An instance of found user profile</returns>
        Task<UserProfileDto> GetProfileByIdAsync(int id);

        /// <summary>
        /// To get an instance of user profile with additional information 
        /// </summary>
        /// <param name="id">The id of user profile that is found</param>
        /// <returns>An instance of found user profile</returns>
        Task<UserProfileDto> GetProfileByIdWithDetailsAsync(int id);

        /// <summary>
        /// To get a collection of user profiles with additional information
        /// </summary>
        /// <returns>Queryable collection of all users profiles</returns>
        Task<IEnumerable<UserProfileDto>> GetAllProfilesWithDetailsAsync();

        /// <summary>
        /// To get an original country id for user
        /// </summary>
        /// <param name="id">The id of user profile</param>
        /// <returns>An instance of found country</returns>
        Task<CountryDto> GetOriginalCountryByUserIdAsync(int id);

        /// <summary>
        /// To get a current country id for user
        /// </summary>
        /// <param name="id">The id of user profile</param>
        /// <returns>An instance of found country</returns>
        Task<CountryDto> GetCurrentCountryByUserIdAsync(int id);

    }
}
