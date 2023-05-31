using AutoMapper;
using BLL.EntitiesDto;
using BLL.Interfaces;
using BLL.Validation;
using DAL.Enteties;
using DAL.Interfaces.BaseInterfaces;

namespace BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _db;

        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for creating of service with given unit of work (database and operations) and automapper profile
        /// </summary>
        /// <param name="database">Instance of unit of work for this service</param>
        /// <param name="mapper">Instance of automapper profile</param>
        public PostService(IUnitOfWork database, IMapper mapper)
        {
            _db = database;
            _mapper = mapper;
        }

        public async Task CreatePostAsync(PostDto item)
        {
            if (item == null)
            {
                throw new HelpSiteException("Post cannot be null");
            }
            if (string.IsNullOrEmpty(item.Content))
            {
                throw new HelpSiteException("Post content cannot be empty");
            }
            var user = await _db.UsersProfiles.GetByIdAsync(item.AuthorId);
            if (user == null)
            {
                throw new NotFoundException($"No user with id: {item.AuthorId} was found");
            }
            var country = await _db.Countries.GetByIdAsync(item.CountryId);
            if (country == null)
            {
                throw new NotFoundException($"No country with id: {item.CountryId} was found");
            }
            var postToCreate = _mapper.Map<Post>(item);
            postToCreate.CreationTime = DateTime.Now;
            await _db.Posts.CreateAsync(postToCreate);
            await _db.SaveAsync();
        }

        public async Task DeletePostAsync(int id)
        {
            var post = await GetPostByIdAsync(id);
            _db.Posts.Delete(id);
            await _db.SaveAsync();
        }

        public async Task<IEnumerable<PostDto>> GetAllCountryPostsByCountryIdAsync(int id)
        {
            var allPosts = await GetAllPostsAsync();
            var country = await _db.Countries.GetByIdAsync(id);
            if (country == null) 
            {
                throw new HelpSiteException($"No country with id: {id} was found");
            }
            var countryPosts = allPosts.Where(post => post.CountryId == id);
            if (countryPosts == null || countryPosts.Count() == 0)
            {
                throw new NotFoundException($"There is no post for country: {country.Name} ");
            }
            return _mapper.Map<IEnumerable<PostDto>>(countryPosts);
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
        {
            var posts = await _db.Posts.GetAllAsync();
            if (posts == null || posts.Count() == 0)
            {
                throw new NotFoundException("There is no post");
            }
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }


        public async Task<IEnumerable<PostDto>> GetAllUserPostsByUserIdAsync(int id)
        {
            var allPosts = await GetAllPostsAsync();
            var user = await _db.Users.GetByIdAsync(id);
            if (user == null)
            {
                throw new HelpSiteException($"No user with id: {id} was found");
            }
            var userPosts = allPosts.Where(post => post.AuthorId == id);
            if (userPosts == null || userPosts.Count() == 0)
            {
                throw new NotFoundException($"There is no post of user {user.UserName} found");
            }
            return _mapper.Map<IEnumerable<PostDto>>(userPosts);
        }

        public async Task<PostDto> GetPostByIdAsync(int id)
        {
            var post = await _db.Posts.GetByIdAsync(id);
            if (post == null)
            {
                throw new NotFoundException($"No post with id: {id} was found");
            }
            return _mapper.Map<PostDto>(post);
        }

        public async Task UpdatePostAsync(PostDto item)
        {
            if (item == null)
            {
                throw new HelpSiteException("Post cannot be null");
            }
            if (string.IsNullOrEmpty(item.Content))
            {
                throw new HelpSiteException("Post content cannot be empty");
            }
            if (await _db.Users.GetByIdAsync(item.AuthorId) == null)
            {
                throw new NotFoundException($"No user with id: {item.AuthorId} was found");
            }
            if (await _db.Countries.GetByIdAsync(item.CountryId) == null)
            {
                throw new NotFoundException($"No country with id: {item.CountryId} was found");
            }
            var postToUpdate = _mapper.Map<Post>(item);
            _db.Posts.Update(postToUpdate);
            await _db.SaveAsync();
        }
    }
}
