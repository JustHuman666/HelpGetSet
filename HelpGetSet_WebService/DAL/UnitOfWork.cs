using DAL.Enteties;
using DAL.Interfaces.BaseInterfaces;
using DAL.Interfaces;
using DAL.Context;

namespace DAL
{
    /// <summary>
    /// Class that represents an unit of work with db context and CRUD repositories
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SiteContext _context;

        /// <summary>
        /// Constructor for creating of an UOW with given db context, user and role repositories
        /// </summary>
        /// <param name="context">The db context for creating of UOW</param>
        /// <param name="userRepository">The instance of user repository</param>
        /// <param name="roleRepository">The instance of role repository</param>
        /// <param name="userProfileRepository">The instance of user profile repository</param>
        /// <param name="chatRepository">The instance of chat repository</param>
        /// <param name="countryRepository">The instance of country repository</param>
        /// <param name="messageRepository">The instance of message repository</param>
        /// <param name="postRepository">The instance of post repository</param>
        /// <param name="migrantRepository">The instance of migrant user repository</param>
        /// <param name="volunteerRepository">The instance of volunteer user repository</param>
        public UnitOfWork(SiteContext context,
                          IUserRepository userRepository,
                          IRoleRepository roleRepository,
                          IUserProfileRepository userProfileRepository,
                          IChatRepository chatRepository,
                          ICountryRepository countryRepository,
                          IMessageRepository messageRepository,
                          IPostRepository postRepository,
                          IMigrantRepository migrantRepository,
                          IVolunteerRepository volunteerRepository,
                          ICountryHistoryRepository countryVersions)
        {
            _context = context;
            Users = userRepository;
            Roles = roleRepository;
            UsersProfiles = userProfileRepository;
            Chats = chatRepository;
            Countries = countryRepository;
            Messages = messageRepository;
            Posts = postRepository;
            Migrants = migrantRepository;
            Volunteers = volunteerRepository;
            CountryVersions = countryVersions;
        }

        public IUserRepository Users { get; }
        public IRoleRepository Roles { get; }
        public IUserProfileRepository UsersProfiles { get; }
        public IChatRepository Chats { get; }
        public ICountryRepository Countries { get; }
        public IMessageRepository Messages { get; }
        public IPostRepository Posts { get; }
        public IMigrantRepository Migrants { get; }
        public IVolunteerRepository Volunteers { get; }
        public ICountryHistoryRepository CountryVersions { get; }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
