using AutoMapper;
using BLL.EntitiesDto;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL_API.Models;
using System.Security.Claims;

namespace PL_API.Controllers
{
    /// <summary>
    /// Controller for working with migrants information
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MigrantController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMigrantService _migrantService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for creating of user controller with given services and mapper profile
        /// </summary>
        /// <param name="mapper">Auto mapper profile for models and dtos</param>
        /// <param name="userService">User service</param>
        /// <param name="migrantService">Migrant service</param>
        public MigrantController(IUserService userService, IMigrantService migrantService, IMapper mapper)
        {
            _userService = userService;
            _migrantService = migrantService;
            _mapper = mapper;
        }

        /// <summary>
        /// To get all migrants information with details
        /// </summary>
        /// <returns>Collection of migrants profiles</returns>
        [HttpGet]
        [Route("All")]
        public async Task<ActionResult<IEnumerable<MigrantModel>>> GetAllMigrantsWithDetails()
        {
            var migrants = await _migrantService.GetAllMigrantsAsync();
            return Ok(_mapper.Map<IEnumerable<MigrantModel>>(migrants));
        }

        /// <summary>
        /// To get migrant profile by their id allowed for all users
        /// </summary>
        /// <param name="id">The id of migrant whose detailed profile should be found</param>
        /// <returns>Found migrant profile</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<MigrantModel>> GetMigrantByIdForAll(int id)
        {
            var migrantDto = await _migrantService.GetMigrantInfoByIdAsync(id);
            return Ok(_mapper.Map<MigrantModel>(migrantDto));
        }

        /// <summary>
        /// To get migrant profile by user id allowed for all users
        /// </summary>
        /// <param name="id">The id of user whose detailed migrant profile should be found</param>
        /// <returns>Found migrant profile</returns>
        [HttpGet]
        [Route("ByUserID/{id}")]
        public async Task<ActionResult<MigrantModel>> GetMigrantByUserIdForAll(int id)
        {
            var migrantDto = await _migrantService.GetMigrantInfoByUserIdAsync(id);
            return Ok(_mapper.Map<MigrantModel>(migrantDto));
        }

        /// <summary>
        /// To get all migrants, who are oficially refugees
        /// </summary>
        /// <returns>Collection of refugees</returns>
        [HttpGet]
        [Route("Refugees")]
        public async Task<ActionResult<IEnumerable<MigrantModel>>> GetAllRefugees()
        {
            var migrants = await _migrantService.GetOfficialRefugeesAsync();
            return Ok(_mapper.Map<IEnumerable<MigrantModel>>(migrants));
        }

        /// <summary>
        /// To get all migrants, who are forced migrants
        /// </summary>
        /// <returns>Collection of forced migrants</returns>
        [HttpGet]
        [Route("AllForced")]
        public async Task<ActionResult<IEnumerable<MigrantModel>>> GetAllForcedMigrants()
        {
            var migrants = await _migrantService.GetForcedMigrantsAsync();
            return Ok(_mapper.Map<IEnumerable<MigrantModel>>(migrants));
        }

        /// <summary>
        /// To get all migrants, who are common migrants
        /// </summary>
        /// <returns>Collection of common migrants</returns>
        [HttpGet]
        [Route("AllCommon")]
        public async Task<ActionResult<IEnumerable<MigrantModel>>> GetAllCommonMigrants()
        {
            var migrants = await _migrantService.GetCommonMigrantsAsync();
            return Ok(_mapper.Map<IEnumerable<MigrantModel>>(migrants));
        }

        /// <summary>
        /// To get housing type of a migrant by their id
        /// </summary>
        /// <param name="id">The id of migrant whose housing type should be found</param>
        /// <returns>Housing type converted to string</returns>
        [HttpGet]
        [Route("{id}/Housing")]
        public async Task<ActionResult<string>> GetUserHousingType(int id)
        {
            var housing = await _migrantService.GetHousingTypeByMigrantIdAsync(id);
            return Ok(housing.ToString());
        }

        /// <summary>
        /// To get family status of a migrant by their id
        /// </summary>
        /// <param name="id">The id of migrant whose family status should be found</param>
        /// <returns>Family status converted to string</returns>
        [HttpGet]
        [Route("{id}/FamilyStatus")]
        public async Task<ActionResult<string>> GetUserFamilyStatus(int id)
        {
            var familyStatus = await _migrantService.GetFamilyStatusByMigrantIdAsync(id);
            return Ok(familyStatus.ToString());
        }

        /// <summary>
        /// To create a migrant profile
        /// </summary>
        /// <param name="migrantModel">Model of migrant for creating with needed data</param>
        [HttpPost]
        [Route("New")]
        public async Task<ActionResult> AddNewMigrant([FromBody] MigrantModel migrantModel)
        {
            await _migrantService.CreateMigrantInfoAsync(_mapper.Map<MigrantDto>(migrantModel));
            return Ok();
        }

        /// <summary>
        /// To update a migrant profile
        /// </summary>
        /// <param name="migrantModel">Model of migrant for updating with needed data</param>
        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> UpdateMigrantInfo([FromBody] MigrantModel migrantModel)
        {
            await _migrantService.UpdateMigrantInfoAsync(_mapper.Map<MigrantDto>(migrantModel));
            return Ok();
        }

        /// <summary>
        /// To delete migrant profile by user themselves or by admin
        /// </summary>
        /// <param name="id">The id of migrant profile to be deleted</param>
        /// <returns>Result status code</returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult> DeleteMigrantById(int id)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userRoles = await _userService.GetAllUserRoles(userId);
            if (userId != id && !userRoles.Contains("Admin"))
            {
                return Forbid();
            }
            await _migrantService.DeleteMigrantInfoAsync(id);
            return Ok();
        }

        /// <summary>
        /// To check if migrant is employed
        /// </summary>
        /// <param name="id">The id of migrant profile be checked</param>
        /// <returns>True if employed</returns>
        [HttpGet]
        [Route("{id}/IsEmployed")]
        public async Task<ActionResult<bool>> IfMigrantIsEmployed(int id)
        {
            var isEmployed = await _migrantService.CheckIfEmployedAsync(id);
            return Ok(isEmployed);
        }
    }
}
