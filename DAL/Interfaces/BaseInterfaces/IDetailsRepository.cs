using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.BaseInterfaces
{
    /// <summary>
    /// Generic interface for repository of some type to get items with additional information
    /// </summary>
    /// <typeparam name="T">Type of entity for repository</typeparam>
    public interface IDetailsRepository<T> where T : class
    {
        /// <summary>
        /// To get an instance of item of chosen type with additional information from DB
        /// </summary>
        /// <param name="id">The id of item that is found</param>
        /// <returns>An instance of found item</returns>
        Task<T> GetByIdWithDetailsAsync(int id);

        /// <summary>
        /// To get aa collection of items of chosen type with additional information from DB
        /// </summary>
        /// <returns>Queryable collection of all items of some type</returns>
        Task<IQueryable<T>> GetAllWithDetailsAsync();
    }
}
