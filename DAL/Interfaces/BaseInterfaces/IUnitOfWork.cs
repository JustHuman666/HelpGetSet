using DAL.Enteties;

namespace DAL.Interfaces.BaseInterfaces
{
    /// <summary>
    /// Interface for unit of work with db context and CRUD repositories
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Repository for users
        /// </summary>
        IUserRepository Users { get; }

        /// <summary>
        /// Repository for users profiles
        /// </summary>
        IUserProfileRepository UsersProfiles { get; }

        /// <summary>
        /// Repository for users roles
        /// </summary>
        IRoleRepository Roles { get; }

        /// <summary>
        /// Repository for users chats
        /// </summary>
        IChatRepository Chats { get; }

        /// <summary>
        /// Repository for messages
        /// </summary>
        IRepository<Message> Messages { get; }

        /// <summary>
        /// Repository for posts
        /// </summary>
        IRepository<Post> Posts { get; }

        /// <summary>
        /// Repository for migrants
        /// </summary>
        IRepository<Migrant> Migrants { get; }

        /// <summary>
        /// Repository for volunteers
        /// </summary>
        IRepository<Volunteer> Volunteers { get; }

        /// <summary>
        /// To save all channges in DB
        /// </summary>
        Task SaveAsync();
    }
}
