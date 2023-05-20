using DAL.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// An interface for roles repository
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// To check if such role exist for this network
        /// </summary>
        /// <param name="name">The name of role that is checked</param>
        /// <returns>Boolean reprenting of result of this operation</returns>
        Task<bool> CheckRoleExistingAsync(string name);

        /// <summary>
        /// To get all possible roles for users
        /// </summary>
        /// <returns>Collection of all possible roles from DB</returns>
        IEnumerable<Role> GetAll();

    }
}
