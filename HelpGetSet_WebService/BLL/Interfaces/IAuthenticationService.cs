using BLL.EntitiesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    /// <summary>
    /// Service interface for user authentication
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// To register a new user of social network
        /// </summary>
        /// <param name="newUser">Instance of new user</param>
        /// <param name="password">The password of this user</param>
        /// <returns>Instance of registered user</returns>
        Task<UserProfileDto> RegisterNewUserAsync(UserDto newUser, string password);

        /// <summary>
        /// To log chosen user in social network 
        /// </summary>
        /// <param name="item">Instance of user who want o log in</param>
        /// <returns>Instance of logged in user with additional information</returns>
        Task<LoginUserInfo> LoginUserAsync(UserDto item);
    }
}
