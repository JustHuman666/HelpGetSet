using AutoMapper;
using BLL.EntitiesDto;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL_API.Models;
using System.Security.Claims;

namespace PL_API.Controllers
{
    /// <summary>
    /// Controller for working with volunteers information
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteerController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IVolunteerService _volunteerService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for creating of user controller with given services and mapper profile
        /// </summary>
        /// <param name="mapper">Auto mapper profile for models and dtos</param>
        /// <param name="userService">User service</param>
        /// <param name="volunteerService">Migrant service</param>
        public VolunteerController(IUserService userService, IVolunteerService volunteerService, IMapper mapper)
        {
            _userService = userService;
            _volunteerService = volunteerService;
            _mapper = mapper;
        }

        /// <summary>
        /// To get all volunteers information with details
        /// </summary>
        /// <returns>Collection of volunteers profiles</returns>
        [HttpGet]
        [Route("All")]
        public async Task<ActionResult<IEnumerable<VolunteerModel>>> GetAllMigrantsWithDetails()
        {
            var volunteers = await _volunteerService.GetAllVolunteersAsync();
            return Ok(_mapper.Map<IEnumerable<VolunteerModel>>(volunteers));
        }

        /// <summary>
        /// To get volunteer profile by their id allowed for all users
        /// </summary>
        /// <param name="id">The id of volunteer whose detailed profile should be found</param>
        /// <returns>Found volunteer profile</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<VolunteerModel>> GetMigrantByIdForAll(int id)
        {
            var volunteerDto = await _volunteerService.GetVolunteerInfoByIdAsync(id);
            return Ok(_mapper.Map<VolunteerModel>(volunteerDto));
        }

        /// <summary>
        /// To get volunteer profile by user id allowed for all users
        /// </summary>
        /// <param name="id">The id of user whose detailed volunteer profile should be found</param>
        /// <returns>Found volunteer profile</returns>
        [HttpGet]
        [Route("ByUserId/{id}")]
        public async Task<ActionResult<VolunteerModel>> GetMigrantByUserIdForAll(int id)
        {
            var volunteerDto = await _volunteerService.GetVolunteerInfoByUserIdAsync(id);
            return Ok(_mapper.Map<VolunteerModel>(volunteerDto));
        }

        /// <summary>
        /// To get all volunteers, who represent organisations
        /// </summary>
        /// <returns>Collection of volunteers, who represent organisations</returns>
        [HttpGet]
        [Route("Organisations")]
        public async Task<ActionResult<IEnumerable<VolunteerModel>>> GetAllOrganistaions()
        {
            var volunteers = await _volunteerService.GetOrganisationsAsync();
            return Ok(_mapper.Map<IEnumerable<VolunteerModel>>(volunteers));
        }

        /// <summary>
        /// To get all volunteers, who are just common people
        /// </summary>
        /// <returns>Collection of volunteers, who are just common people</returns>
        [HttpGet]
        [Route("CommonVolunteers")]
        public async Task<ActionResult<IEnumerable<VolunteerModel>>> GetAllCommonVolunteer()
        {
            var volunteers = await _volunteerService.GetJustVolunteersAsync();
            return Ok(_mapper.Map<IEnumerable<VolunteerModel>>(volunteers));
        }

        /// <summary>
        /// To get all volunteers, who could help with translation
        /// </summary>
        /// <returns>Collection of volunteers, who could help with translation
        [HttpGet]
        [Route("Translators")]
        public async Task<ActionResult<IEnumerable<VolunteerModel>>> GetAllTranslators()
        {
            var volunteers = await _volunteerService.GetTranslatorsAsync();
            return Ok(_mapper.Map<IEnumerable<VolunteerModel>>(volunteers));
        }

        /// <summary>
        /// To get all volunteers, who could help with housing
        /// </summary>
        /// <returns>Collection of volunteers, who could help with housing
        [HttpGet]
        [Route("ForHousing")]
        public async Task<ActionResult<IEnumerable<VolunteerModel>>> GetAllForHousing()
        {
            var volunteers = await _volunteerService.GetVolunteersForHousingAsync();
            return Ok(_mapper.Map<IEnumerable<VolunteerModel>>(volunteers));
        }

        /// <summary>
        /// To create a volunteer profile
        /// </summary>
        /// <param name="volunteerModel">Model of volunteer for creating with needed data</param>
        [HttpPost]
        [Route("New")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult> AddNewVolunteer([FromBody] VolunteerModel volunteerModel)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            volunteerModel.UserId = userId;
            await _volunteerService.CreateVolunteerInfoAsync(_mapper.Map<VolunteerDto>(volunteerModel));
            return Ok();
        }

        /// <summary>
        /// To update a volunteer profile
        /// </summary>
        /// <param name="volunteerModel">Model of volunteer for updating with needed data</param>
        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult> UpdateVolunteerInfo([FromBody] VolunteerModel volunteerModel)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userRoles = await _userService.GetAllUserRoles(userId);
            if (volunteerModel.UserId != userId && !userRoles.Contains("Admin"))
            {
                return Forbid();
            }
            await _volunteerService.CreateVolunteerInfoAsync(_mapper.Map<VolunteerDto>(volunteerModel));
            return Ok();
        }

        /// <summary>
        /// To delete volunteer profile by user themselves or by admin
        /// </summary>
        /// <param name="id">The id of volunteer profile to be deleted</param>
        /// <returns>Result status code</returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult> DeleteVolunteerById(int id)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userRoles = await _userService.GetAllUserRoles(userId);
            if (userId != id && !userRoles.Contains("Admin"))
            {
                return Forbid();
            }
            await _volunteerService.DeleteVolunteerInfoAsync(id);
            return Ok();
        }
    }
}
