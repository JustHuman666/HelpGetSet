using AutoMapper;
using BLL.EntitiesDto;
using BLL.Interfaces;
using BLL.JvtAuthOptions;
using BLL.Validation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthentificationService : IAuthenticationService
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for creating of service with given unit of work (database and operations) and automapper profile
        /// </summary>
        /// <param name="userService">Instance of unit of work for this service</param>
        /// <param name="mapper">Instance of automapper profile</param>
        public AuthentificationService(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<LoginUserInfo> LoginUserAsync(UserDto item)
        {
            var symmetricSecurityKey = AuthOptions.GetSymmetricSecurityKey();
            var user = await _userService.GetUserByPhoneNumberAsync(item.PhoneNumber);
            if (user == null)
            {
                throw new NotFoundException("User does not exist");
            }
            var roles = await _userService.GetAllUserRoles(item.Id);

            var authentificationClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            authentificationClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var claimsIdentity = new ClaimsIdentity(authentificationClaims, "Token",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            var dateTimeNow = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                claims: claimsIdentity.Claims,
                audience: AuthOptions.Audience,
                notBefore: dateTimeNow,
                issuer: AuthOptions.Issuer,
                expires: dateTimeNow.AddDays(1),
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256));

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new LoginUserInfo(jwtToken, user.Id);

        }

        public async Task<UserProfileDto> RegisterNewUserAsync(UserDto newUser, string password)
        {
            await _userService.CreateUserWithRoleAsync(newUser, password, "Registered");
            var user = await _userService.GetUserByPhoneNumberAsync(newUser.PhoneNumber);
            return _mapper.Map<UserProfileDto>(user.UserProfile);
        }
    }
}
