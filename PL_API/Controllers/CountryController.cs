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
    /// Controller for working with countries
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController: ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly ICountryChangesHistoryService _countryChangesService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for creating of user controller with given services and mapper profile
        /// </summary>
        /// <param name="mapper">Auto mapper profile for models and dtos</param>
        /// <param name="_countryService">User service</param>
        /// <param name="_countryChangesService">User profile service</param>
        public CountryController(ICountryService countryService,
                                 ICountryChangesHistoryService countryChangesService,
                                 IUserService userService,
                                 IMapper mapper)
        {
            _countryService = countryService;
            _countryChangesService = countryChangesService;
            _userService = userService;
            _mapper = mapper;
        }


        /// <summary>
        /// To get all countries
        /// </summary>
        /// <returns>Collection of countries</returns>
        [HttpGet]
        [Route("All")]
        public async Task<ActionResult<IEnumerable<CountryModel>>> GetAllCountries()
        {
            var countries = await _countryService.GetAllCountriesAsync();
            return Ok(_mapper.Map<IEnumerable<CountryModel>>(countries));
        }

        /// <summary>
        /// To get country by its id allowed for all users
        /// </summary>
        /// <param name="id">The id of country which should be found</param>
        /// <returns>Found country</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CountryModel>> GetCountryByIdForAll(int id)
        {
            var country = await _countryService.GetCountryByIdAsync(id);
            return Ok(_mapper.Map<CountryModel>(country));
        }

        /// <summary>
        /// To get country by its original name
        /// </summary>
        /// <param name="name">The name of country which should be found</param>
        /// <returns>Found country</returns>
        [HttpGet]
        [Route("Name/{name}")]
        public async Task<ActionResult<CountryModel>> GetCountryByName(string name)
        {
            var country = await _countryService.GetCountryByNameAsync(name);
            return Ok(_mapper.Map<CountryModel>(country));
        }

        /// <summary>
        /// To get country by its short name
        /// </summary>
        /// <param name="name">The short name of country which should be found</param>
        /// <returns>Found country</returns>
        [HttpGet]
        [Route("ShortName/{name}")]
        public async Task<ActionResult<CountryModel>> GetCountryByShortName(string name)
        {
            var country = await _countryService.GetCountryByShortNameAsync(name);
            return Ok(_mapper.Map<CountryModel>(country));
        }

        /// <summary>
        /// To get users who are originally from a country by country id
        /// </summary>
        /// <param name="id">The id of a country which users should be found</param>
        /// <returns>Found users from the country</returns>
        [HttpGet]
        [Route("UsersFrom/{id}")]
        public async Task<ActionResult<IEnumerable<UserProfileModel>>> GetUsersFromCountry(int id)
        {
            var users = await _countryService.GetUsersFromCountryByIdAsync(id);
            return Ok(_mapper.Map<IEnumerable<UserProfileModel>>(users));
        }

        /// <summary>
        /// To get users who are currently in a country by country id
        /// </summary>
        /// <param name="id">The id of a country which users should be found</param>
        /// <returns>Found users who are in the country</returns>
        [HttpGet]
        [Route("UsersIn/{id}")]
        public async Task<ActionResult<IEnumerable<UserProfileModel>>> GetUsersInCountry(int id)
        {
            var users = await _countryService.GetUsersInCountryByIdAsync(id);
            return Ok(_mapper.Map<IEnumerable<UserProfileModel>>(users));
        }

        /// <summary>
        /// To add new country
        /// </summary>
        /// <param name="countryChangesModel">Model of country for creating with needed data</param>
        [HttpPost]
        [Route("Add")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult> AddNewCountryByUser([FromBody] CountryChangesHistoryModel countryChangesModel)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var country = new CountryModel() { Name = countryChangesModel.Name, ShortName = countryChangesModel.ShortName };
            await _countryService.CreateCountryAsync(_mapper.Map<CountryDto>(country));
            var createdCountry = await _countryService.GetCountryByNameAsync(countryChangesModel.Name);
            countryChangesModel.CountryId = createdCountry.Id;
            countryChangesModel.AuthorId = userId;
            await _countryChangesService.CreateCountryVersionAsync(_mapper.Map<CountryChangesHistoryDto>(countryChangesModel));
            return Ok();
        }

        /// <summary>
        /// To rename country if the same author or by admin
        /// </summary>
        /// <param name="countryModel">Model of country for with data for renaming</param>
        [HttpPut]
        [Route("Rename")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult> RenameCountry([FromBody] CountryModel countryModel)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userRoles = await _userService.GetAllUserRoles(userId);
            var countryVersions = await _countryChangesService.GetAllCountryInfoVersionsByCountryIdAsync(countryModel.Id);
            if ((countryVersions.OrderBy(version => version.ChangeTime).First().AuthorId != userId)
                && !userRoles.Contains("Admin"))
            {
                return BadRequest($"Only admin or creator of the country info can rename it.");
            }
            
            await _countryService.UpdateCountryNamesAsync(_mapper.Map<CountryDto>(countryModel));
            return Ok();
        }

        /// <summary>
        /// To update country info
        /// </summary>
        /// <param name="changes">Model of country for with data for updating</param>
        [HttpPost]
        [Route("ChangeInfo")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult> UpdateCountryInfo([FromBody] CountryChangesHistoryModel changes)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            changes.AuthorId = userId;
            await _countryChangesService.CreateCountryVersionAsync(_mapper.Map<CountryChangesHistoryDto>(changes));
            return Ok();
        }

        /// <summary>
        /// To get country version by its id
        /// </summary>
        /// <param name="id">The id of country version which should be found</param>
        /// <returns>Found country version</returns>
        [HttpGet]
        [Route("Version/{id}")]
        public async Task<ActionResult<CountryChangesHistoryModel>> GetCountryVersionByIdForAll(int id)
        {
            var version = await _countryChangesService.GetVersionByIdAsync(id);
            return Ok(_mapper.Map<CountryChangesHistoryModel>(version));
        }

        /// <summary>
        /// To get all versions of a country by id
        /// </summary>
        /// <param name="id">The id of a country which versions should be found</param>
        /// <returns>Found versions of the country</returns>
        [HttpGet]
        [Route("{id}/AllVersions")]
        public async Task<ActionResult<IEnumerable<CountryChangesHistoryModel>>> GetCountryVersions(int id)
        {
            var versions = await _countryChangesService.GetAllCountryInfoVersionsByCountryIdAsync(id);
            return Ok(_mapper.Map<IEnumerable<CountryChangesHistoryModel>>(versions));
        }

        /// <summary>
        /// To get the last version of a country by id
        /// </summary>
        /// <param name="id">The id of a country</param>
        /// <returns>Found version of the country</returns>
        [HttpGet]
        [Route("{id}/LastVersion")]
        public async Task<ActionResult<CountryChangesHistoryModel>> GetCountryLastVersion(int id)
        {
            var version = await _countryChangesService.GetLastCountryInfoByIdAsync(id);
            return Ok(_mapper.Map<CountryChangesHistoryModel>(version));
        }

        /// <summary>
        /// To approve chosen version
        /// </summary>
        /// <param name="id">Id of version to be approved</param>
        [HttpPut]
        [Route("ApproveVersion/{id}")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult> ApproveVersion(int id)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _countryChangesService.ApproveVersionByIdAsync(id, userId);
            return Ok();
        }

        /// <summary>
        /// To disapprove chosen version
        /// </summary>
        /// <param name="id">Id of version to be disapproved</param>
        [HttpPut]
        [Route("DisapproveVersion/{id}")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult> DisapproveVersion(int id)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _countryChangesService.DisapproveVersionByIdAsync(id, userId);
            return Ok();
        }

        /// <summary>
        /// To delete country version
        /// </summary>
        /// <param name="id">The id of version to be deleted</param>
        /// <returns>Result status code</returns>
        [HttpDelete]
        [Route("Version/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteVersionByAdmin(int id)
        {
            await _countryChangesService.DeleteCountryInfoVersionAsync(id);
            return Ok();
        }

        /// <summary>
        /// To delete country with all country versions by admin
        /// </summary>
        /// <param name="id">The id of country to be deleted</param>
        /// <returns>Result status code</returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteCountryByAdmin(int id)
        {
            await _countryChangesService.DeleteCountryInfoAsync(id);
            await _countryService.DeleteCountryAsync(id);
            return Ok();
        }

        /// <summary>
        /// To check if user already approved or disapproved
        /// </summary>
        /// <param name="id">The id of a version to be checked</param>
        /// <returns>True if already checked</returns>
        [HttpGet]
        [Route("Version/{id}/IsChecked")]
        public async Task<ActionResult<bool>> IfUserCheckedVersion(int id)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var isChecked = await _countryChangesService.CheckIfUserAlreadyCheckedVersionAsync(id, userId);
            return Ok(isChecked);
        }

        /// <summary>
        /// To define if user approved or disapproved
        /// </summary>
        /// <param name="id">The id of a version to be checked</param>
        /// <returns>True if approved, false if not</returns>
        [HttpGet]
        [Route("Version/{id}/IsApproved")]
        public async Task<ActionResult<bool>> IfUserApprovedOrDisapprovedVersion(int id)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var approved = await _countryChangesService.CheckIfUserApprovedOrDisapprovedAsync(id, userId);
            return Ok(approved);
        }
    }
}