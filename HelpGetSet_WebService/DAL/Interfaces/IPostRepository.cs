using DAL.Enteties;
using DAL.Interfaces.BaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPostRepository: IRepository<Post>
    {
        /// <summary>
        /// To get a collection of posts from DB by an author's id
        /// </summary>
        /// <param name="id">Id of author of post which are being is looked for</param>
        /// <returns>A collection of found posts</returns>
        Task<IQueryable<Post>> GetByAuthorIdAsync(int id);

        /// <summary>
        /// To get a collection of posts from DB by a country
        /// </summary>
        /// <param name="id">Id of country of post which are being is looked for</param>
        /// <returns>A collection of found posts</returns>
        Task<IQueryable<Post>> GetByCountryIdAsync(int id);

        Task<IQueryable<Post>> GetMigrantsPostsAsync();

        Task<IQueryable<Post>> GetVolunteersPostsAsync();
    }
}
