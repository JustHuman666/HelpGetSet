using DAL.Enteties;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DAL.Repositories
{
    /// <summary>
    /// Class that represents a role repository
    /// </summary>
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<Role> _roleManager;

        /// <summary>
        /// Constructor for creating a repository with given db context
        /// </summary>
        /// <param name="context">The instance of db context for the repository</param>
        public RoleRepository(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CheckRoleExistingAsync(string name)
        {
            return await _roleManager.RoleExistsAsync(name);
        }

        public IEnumerable<Role> GetAll()
        {
            return _roleManager.Roles;
        }
    }
}
