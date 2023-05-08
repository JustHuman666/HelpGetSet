namespace DAL.Interfaces.BaseInterfaces
{
    /// <summary>
    /// Generic interface for repository of some type
    /// </summary>
    /// <typeparam name="T">Type of entity for repository</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// To add a new instance of some type to DB
        /// </summary>
        /// <param name="item">The instance of new item that should be created and added to DB</param>
        Task CreateAsync(T item);

        /// <summary>
        /// To update some information about chosen item in DB
        /// </summary>
        /// <param name="item">The instance of item of some type that should be changed</param>
        void Update(T item);

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
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// To get a collection of all items of chosen type from DB
        /// </summary>
        /// <returns>Queryable collection of items of chosen type</returns>
        Task<IQueryable<T>> GetAllAsync();

    }
}
