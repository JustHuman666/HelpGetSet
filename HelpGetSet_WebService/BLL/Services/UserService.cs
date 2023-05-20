using AutoMapper;
using BLL.EntitiesDto;
using BLL.Interfaces;
using BLL.Validation;
using DAL.Enteties;
using DAL.Interfaces.BaseInterfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    /// <summary>
    /// Service for working with users and user friends
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _db;

        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for creating of service with given unit of work (database and operations) and automapper profile
        /// </summary>
        /// <param name="database">Instance of unit of work for this service</param>
        /// <param name="mapper">Instance of automapper profile</param>
        public UserService(IUnitOfWork database, IMapper mapper)
        {
            _db = database;
            _mapper = mapper;
        }

        public async Task<IdentityResult> AddUserToRoleAsync(int id, string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                throw new HelpSiteException("Role name cannot be null or empty");
            }
            var isRoleExist = await _db.Roles.CheckRoleExistingAsync(role);
            if (!isRoleExist)
            {
                throw new NotFoundException("Role does not exist");
            }
            var user = await _db.Users.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User does not exist");
            }
            var result = await _db.Users.AddRoleAsync(user, role);
            await _db.SaveAsync();
            return result;
        }

        public async Task<IdentityResult> ChangeUserPasswordAsync(int id, string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
            {
                throw new HelpSiteException("Password cannot be empty");
            }
            if (oldPassword.Contains(" ") || newPassword.Contains(" "))
            {
                throw new HelpSiteException("Password cannot contain empty spaces");
            }
            var user = await _db.Users.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User does not exist");
            }
            var result = await _db.Users.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!result.Succeeded)
            {
                throw new HelpSiteException("Old or new password is incorrect. New password should contain at least 8 characters," +
                    "and include uppercase and special characters");
            }
            await _db.SaveAsync();
            return result;
        }

        public async Task<bool> CheckUserPasswordAsync(int id, string password)
        {
            var user = await _db.Users.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User does not exist");
            }
            var result = await _db.Users.CheckPasswordAsync(user, password);
            return result;
        }

        public async Task CreateUserWithRoleAsync(UserDto newUser, string password, string role)
        {
            if (newUser == null)
            {
                throw new HelpSiteException("User cannot be null");
            }
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                throw new HelpSiteException("Password and role name cannot be empty");
            }
            if (string.IsNullOrEmpty(newUser.FirstName)
                || string.IsNullOrEmpty(newUser.LastName)
                || string.IsNullOrEmpty(newUser.UserName)
                || string.IsNullOrEmpty(newUser.PhoneNumber))
            {
                throw new HelpSiteException("Users parameters cannot be empty");
            }
            if (password.Contains(" ")
                || newUser.FirstName.Contains(" ")
                || newUser.LastName.Contains(" ")
                || newUser.UserName.Contains(" ")
                || newUser.PhoneNumber.Contains(" "))
            {
                throw new HelpSiteException("Users parameters and password cannot contain empty spaces");
            }
            if (newUser.PhoneNumber.Contains("+"))
            {
                throw new HelpSiteException("User phone number cannot have non-numeric chatacters");
            }
            var isRoleExist = await _db.Roles.CheckRoleExistingAsync(role);
            if (!isRoleExist)
            {
                throw new NotFoundException("Role does not exist");
            }
            var existWithPhoneUser = await _db.Users.GetByPhoneNumberAsync(newUser.PhoneNumber);
            if (existWithPhoneUser != null)
            {
                throw new HelpSiteException("This phone number is already occupied");
            }
            var existWithUsername = _db.Users.GetAll().FirstOrDefault(user => user.UserName == newUser.UserName);
            if (existWithUsername != null)
            {
                throw new HelpSiteException("This username is already occupied");
            }
            var userToCreate = _mapper.Map<User>(newUser);
            var result = await _db.Users.CreateAsync(userToCreate, password);
            if (!result.Succeeded)
            {
                throw new HelpSiteException("Password is incorrect. New password should contain at least 8 characters," +
                    "and include uppercase and special characters");
            }
            var createdUser = await _db.Users.GetByPhoneNumberAsync(userToCreate.PhoneNumber);
            await _db.Users.AddRoleAsync(createdUser, role);
            await _db.SaveAsync();
        }

        public async Task DeleteUserByIdAsync(int id)
        {
            var user = await _db.Users.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User does not exist");
            }
            await _db.Users.DeleteAsync(id);
            await _db.SaveAsync();
        }

        public async Task<IEnumerable<string>> GetAllUserRoles(int id)
        {
            var user = await _db.Users.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User does not exist");
            }
            var roles = await _db.Users.GetAllUserRoles(user);
            return roles;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _db.Users.GetAll();
            if (users == null || users.Count() == 0)
            {
                throw new NotFoundException("Any user does not exist");
            }
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public IEnumerable<UserDto> GetAllUsersWithDetails()
        {
            var users = _db.Users.GetAllWithDetails();
            if (users == null || users.Count() == 0)
            {
                throw new NotFoundException("Any user does not exist");
            }
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public IEnumerable<UserDto> GetUsersByFirstAndLastName(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                throw new HelpSiteException("Firstname and lastname cannot be empty");
            }
            if (firstName.Contains(" ") || lastName.Contains(" "))
            {
                throw new HelpSiteException("Firstname and lastname cannot contain empty spaces");
            }
            var users = _db.Users.GetAll().Where(user => user.UserProfile.FirstName == firstName && user.UserProfile.LastName == lastName);
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _db.Users.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User does not exist");
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByIdWithDetailsAsync(int id)
        {
            var user = await _db.Users.GetByIdWithDetailsAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User does not exist");
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByPhoneNumberAsync(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                throw new HelpSiteException("Phone number cannot be empty");
            }
            if (phoneNumber.Contains(" "))
            {
                throw new HelpSiteException("Phone number cannot contain empty spaces");
            }
            var user = await _db.Users.GetByPhoneNumberAsync(phoneNumber);
            if (user == null)
            {
                throw new NotFoundException("User does not exist");
            }
            return _mapper.Map<UserDto>(user);
        }

        public UserDto GetUserByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new HelpSiteException("Username  cannot be empty");
            }
            if (userName.Contains(" "))
            {
                throw new HelpSiteException("Username cannot contain empty spaces");
            }
            var user = _db.Users.GetAll().FirstOrDefault(user => user.UserName == userName);
            if (user == null)
            {
                throw new NotFoundException("User does not exist");
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateUserInfoAsync(UserDto user)
        {
            if (user == null)
            {
                throw new HelpSiteException("User cannot be null");
            }
            if (string.IsNullOrEmpty(user.FirstName)
                || string.IsNullOrEmpty(user.LastName)
                || string.IsNullOrEmpty(user.UserName)
                || string.IsNullOrEmpty(user.PhoneNumber))
            {
                throw new HelpSiteException("Users parameters cannot be empty");
            }
            if (user.FirstName.Contains(" ")
                || user.LastName.Contains(" ")
                || user.UserName.Contains(" ")
                || user.PhoneNumber.Contains(" "))
            {
                throw new HelpSiteException("Users parameters and password cannot contain empty spaces");
            }
            if (user.PhoneNumber.Contains("+"))
            {
                throw new HelpSiteException("User phone number cannot have non-numeric chatacters");
            }
            var existWithPhoneUser = await _db.Users.GetByPhoneNumberAsync(user.PhoneNumber);
            if (existWithPhoneUser != null && user.Id != existWithPhoneUser.Id)
            {
                throw new HelpSiteException("This phone number is already occupied");
            }
            var existWithUsername = _db.Users.GetAll().FirstOrDefault(us => us.UserName == user.UserName);
            if (existWithUsername != null && user.Id != existWithUsername.Id)
            {
                throw new HelpSiteException("This username is already occupied");
            }
            var userToUpdate = _mapper.Map<User>(user);
            await _db.Users.UpdateAsync(userToUpdate);
            await _db.SaveAsync();
        }
    }
}
