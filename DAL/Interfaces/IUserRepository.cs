using DAL.Enteties;
using Microsoft.AspNetCore.Identity;

namespace DAL.Interfaces
{

    /// <summary>
    /// An interface for user repository
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// To add a new instance of user to DB
        /// </summary>
        /// <param name="item">The instance of new user</param>
        /// <param name="password">The password of new user</param>
        /// <returns>The identity result with information about final result</returns>
        Task<IdentityResult> CreateAsync(User item, string password);

        /// <summary>
        /// To change password for chosen user
        /// </summary>
        /// <param name="item">The instance of user whose password should be changed</param>
        /// <param name="oldPassword">Current password of this user</param>
        /// <param name="newPassword">New password that will be next</param>
        /// <returns>The identity result with information about final result</returns>
        Task<IdentityResult> ChangePasswordAsync(User item, string oldPassword, string newPassword);

        /// <summary>
        /// To get an instance of the user with such phone number
        /// </summary>
        /// <param name="phoneNumber">Phone number of user who is found</param>
        /// <returns>An instance of found user</returns>
        Task<User> GetByPhoneNumberAsync(string phoneNumber);

        /// <summary>
        /// To check if password is correct and true for this user
        /// </summary>
        /// <param name="item">The instance of user whose password is compared with given</param>
        /// <param name="password">The password which is checked</param>
        /// <returns>Boolean representing of result of this operation</returns>
        Task<bool> CheckPasswordAsync(User item, string password);

        /// <summary>
        /// Add chosen user to chosen role
        /// </summary>
        /// <param name="item">The instance of user who should be added to the role</param>
        /// <param name="role">The name of role for this user</param>
        /// <returns>The identity result with information about final result</returns>
        Task<IdentityResult> AddRoleAsync(User item, string role);

        /// <summary>
        /// To get all roles for chosen user
        /// </summary>
        /// <param name="item">The instance of user whose roles are found</param>
        /// <returns>Collection of all roles names</returns>
        Task<IEnumerable<string>> GetAllUserRoles(User item);

        /// <summary>
        ///  To update some information about chosen user in DB
        /// </summary>
        /// <param name="item">The instance of user that should be changed</param>
        Task UpdateAsync(User item);

        /// <summary>
        /// To delete chosen user form DB by user id
        /// </summary>
        /// <param name="id">Id of user who should be deleted</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// To get an instance of user from DB by user id
        /// </summary>
        /// <param name="id">Id of user who is found</param>
        /// <returns>An instance of found user</returns>
        Task<User> GetByIdAsync(int id);

        /// <summary>
        /// To get a collection of all users from DB
        /// </summary>
        /// <returns>Queryable collection of users</returns>
        IQueryable<User> GetAll();

        /// <summary>
        /// To get an instance of user with additional information from DB
        /// </summary>
        /// <param name="id">The id of user that is found</param>
        /// <returns>An instance of found user</returns>
        Task<User> GetByIdWithDetailsAsync(int id);

        /// <summary>
        /// To get an instance of user with additional information from DB by username
        /// </summary>
        /// <param name="username">The username of user that is found</param>
        /// <returns>An instance of found user</returns>
        Task<User> GetByUsernameWithDetailsAsync(string username);

        /// <summary>
        /// To get a collection of users with additional information from DB
        /// </summary>
        /// <returns>Collection of found users with all details</returns>
        IQueryable<User> GetAllWithDetails();
    }
}
