using DAL.Enteties;
using DAL.Interfaces.BaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IMessageRepository
    {
        /// <summary>
        /// To add a new instance of some type to DB
        /// </summary>
        /// <param name="item">The instance of new item that should be created and added to DB</param>
        Task<Message> CreateAsync(Message item);

        /// <summary>
        /// To update some information about chosen item in DB
        /// </summary>
        /// <param name="item">The instance of item of some type that should be changed</param>
        void Update(Message item);

        /// <summary>
        /// To delete chosen item form DB by its unique id
        /// </summary>
        /// <param name="id">Id of item that should be deleted</param>
        void Delete(int id);

        /// <summary>
        /// To get an instance of item of some type from DB by its id
        /// </summary>
        /// <param name="id">Id of item that is found</param>
        /// <returns>An instance of found item</returns>
        Task<Message> GetByIdAsync(int id);

        /// <summary>
        /// To get a collection of all items of chosen type from DB
        /// </summary>
        /// <returns>Queryable collection of items of chosen type</returns>
        Task<IQueryable<Message>> GetAllAsync();

        /// <summary>
        /// To get a collection of all items of chosen type from DB
        /// </summary>
        /// <returns>Queryable collection of items of chosen type</returns>
        Task<IQueryable<Message>> GetMessagesByChatAsync(int id);
    }
}
