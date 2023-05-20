using AutoMapper;
using BLL.EntitiesDto;
using BLL.Interfaces;
using BLL.Services;
using IPinfo;
using IPinfo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using PL_API.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace PL_API.Controllers
{
    /// <summary>
    /// Controller for working with posts
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for creating of user controller with given services and mapper profile
        /// </summary>
        /// <param name="mapper">Auto mapper profile for models and dtos</param>
        /// <param name="userService">User service</param>
        /// <param name="postService">Post service</param>
        public PostController(IUserService userService, IPostService postService, ICountryService countryService, IMapper mapper)
        {
            _userService = userService;
            _postService = postService;
            _countryService = countryService;
            _mapper = mapper;
        }

        /// <summary>
        /// To get all posts with details
        /// </summary>
        /// <returns>Collection of posts</returns>
        [HttpGet]
        [Route("All")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetAllPostsWithDetails()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(_mapper.Map<IEnumerable<PostModel>>(posts));
        }

        /// <summary>
        /// To get post by its id allowed for all users
        /// </summary>
        /// <param name="id">The id of post which should be found</param>
        /// <returns>Found post</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<PostModel>> GetPostByIdForAll(int id)
        {
            var postDto = await _postService.GetPostByIdAsync(id);
            return Ok(_mapper.Map<PostModel>(postDto));
        }

        /// <summary>
        /// To get all posts of user by their id allowed
        /// </summary>
        /// <param name="id">The id of user whose posts should be found</param>
        /// <returns>Found posts</returns>
        [HttpGet]
        [Route("AllOfUser/{id}")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetPostsByUserIdForAll(int id)
        {
            var posts = await _postService.GetAllUserPostsByUserIdAsync(id);
            return Ok(_mapper.Map<IEnumerable<PostModel>>(posts));
        }

        /// <summary>
        /// To get all posts related to a country by the country id
        /// </summary>
        /// <param name="id">The id of country for posts should be found</param>
        /// <returns>Found posts</returns>
        [HttpGet]
        [Route("AllOfCountry/{id}")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetPostsByCountryIdForAll(int id)
        {
            var posts = await _postService.GetAllCountryPostsByCountryIdAsync(id);
            return Ok(_mapper.Map<IEnumerable<PostModel>>(posts));
        }

        /// <summary>
        /// To create a post
        /// </summary>
        /// <param name="postModel">Model of migrant for creating with needed data</param>
        [HttpPost]
        [Route("New")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult> AddNewPost([FromBody] PostModel postModel)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            postModel.AuthorId = userId;
            await _postService.CreatePostAsync(_mapper.Map<PostDto>(postModel));
            return Ok();
        }

        /// <summary>
        /// To update a post
        /// </summary>
        /// <param name="postModel">Model of post for updating with needed data</param>
        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult> UpdatePost([FromBody] PostModel postModel)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (postModel.AuthorId != userId)
            {
                return Forbid();
            }
            await _postService.UpdatePostAsync(_mapper.Map<PostDto>(postModel));
            return Ok();
        }

        /// <summary>
        /// To get suitable posts with details
        /// </summary>
        /// <param name="ipAddress">The ip address of user for posts should be found</param>
        /// <returns>Collection of found posts</returns>
        [HttpGet]
        [Route("Suitable")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetSuitablePosts(string ipAddress)
        {
            string token = "28885a9b4f12fb";
            IPinfoClient client = new IPinfoClient.Builder()
                .AccessToken(token)
                .Build();
            IPResponse ipResponse = await client.IPApi.GetDetailsAsync(ipAddress);
            string countryName = ipResponse.CountryName;
            var country = await _countryService.GetCountryByNameAsync(countryName);
            if (country != null) 
            {
                var countryPosts = await _postService.GetAllCountryPostsByCountryIdAsync(country.Id);
                return Ok(_mapper.Map<IEnumerable<PostModel>>(countryPosts.OrderByDescending(post => post.CreationTime)));
            }
            else
            {
                var allPosts = await _postService.GetAllPostsAsync();
                var sortedPosts = allPosts.Take(20).OrderByDescending(post => post.CreationTime);
                return Ok(_mapper.Map<IEnumerable<PostModel>>(sortedPosts));
            }
        }

        /// <summary>
        /// To delete a post
        /// </summary>
        /// <param name="id">The id of post to be deleted</param>
        /// <returns>Result status code</returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeletePost(int id)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userRoles = await _userService.GetAllUserRoles(userId);
            var postDto = await _postService.GetPostByIdAsync(id);
            if (postDto.AuthorId != userId && !userRoles.Contains("Admin"))
            {
                return Forbid($"Only admin or creator of the country info can rename it.");
            }
            return Ok();
        }
    }
}
