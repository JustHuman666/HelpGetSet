using BLL.EntitiesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    /// <summary>
    /// Service interface for working with chats
    /// </summary>
    public interface IChatService
    {
        /// <summary>
        /// To get all users of chosen chat
        /// </summary>
        /// <param name="id">The id of chat</param>
        /// <returns>Collection of users who are in this chat</returns>
        Task<IEnumerable<UserProfileDto>> GetAllUsersOfChatAsync(int id);

        /// <summary>
        /// To get all chats of chosen user
        /// </summary>
        /// <param name="id">The id of user whose chats should be found</param>
        /// <returns>Collection of chats of this user</returns>
        Task<IEnumerable<ChatDto>> GetAllChatsByUserIdAsync(int id);

        /// <summary>
        /// To add a new instance of some type to DB
        /// </summary>
        /// <param name="item">The instance of new item that should be created and added to DB</param>
        Task CreateChatAsync(ChatDto item);

        /// <summary>
        /// To update some information about chosen item in DB
        /// </summary>
        /// <param name="chatId">The id of chat that should be changed</param>
        /// <param name="chatName">The new name for chosen chat that should be changed</param>
        Task RenameChatAsync(int chatId, string chatName);

        /// <summary>
        /// To add new user in a chat
        /// </summary>
        /// <param name="chatId">The id of chat where user should be added</param>
        /// <param name="userId">The id of user who should be added</param>
        Task AddUserInChatAsync(int chatId, int userId);

        /// <summary>
        /// To delete chosen user from a chat
        /// </summary>
        /// <param name="chatId">The id of chat where user should be deleted</param>
        /// <param name="userId">The id of user who should be deleted</param>
        /// <param name="whoDelete">The id of user who want to delete user from chat</param>
        Task DeleteUserFromChatAsync(int chatId, int userId, int whoDelete);

        /// <summary>
        /// To clear a history of messages of chosen chat
        /// /// </summary>
        /// <param name="id">The id of chat which history should be cleared</param>
        /// <param name="userId">The id of user who want to do that</param>
        Task ClearChatHistoryByIdAsync(int id, int userId);

        /// <summary>
        /// To delete chosen chat by its unique id
        /// </summary>
        /// <param name="id">Id of chat that should be deleted</param>
        /// <param name="userId">The id of user who want to do that</param>
        Task DeleteChatAsync(int id, int userId);

        /// <summary>
        /// To get an instance of chat by its id
        /// </summary>
        /// <param name="id">Id of chat that is found</param>
        /// <returns>An instance of found chat</returns>
        Task<ChatDto> GetChatByIdAsync(int id);

        /// <summary>
        /// To get a collection of all chats
        /// </summary>
        /// <returns>Queryable collection of chats</returns>
        Task<IEnumerable<ChatDto>> GetAllChatsAsync();

        /// <summary>
        /// To get an instance of chat with additional information 
        /// </summary>
        /// <param name="id">The id of chat that is finding</param>
        /// <returns>An instance of found chat</returns>
        Task<ChatDto> GetChatByIdWithDetailsAsync(int id);

        /// <summary>
        /// To change the status of chosen user in chosen chat for admin
        /// </summary>
        /// <param name="userId">The id of chat that is finding</param>
        /// <param name="adminId">The id of user who want to change status and should be admin</param>
        /// <param name="chatId">The id of chat where chosen user wanted to be admin</param>
        Task SetAdminStatusToUserAsync(int userId, int adminId, int chatId);

        /// <summary>
        /// To change the status of chosen user in chosen chat for default
        /// </summary>
        /// <param name="userId">The id of chat that is finding</param>
        /// <param name="adminId">The id of user who want to change status and should be admin</param>
        /// <param name="chatId">The id of chat where chosen user wanted to be admin</param>
        Task SetDefaultStatusToUserAsync(int userId, int adminId, int chatId);

        /// <summary>
        /// To get a collection of chats with additional information 
        /// </summary>
        /// <returns>Queryable collection of all chats</returns>
        Task<IEnumerable<ChatDto>> GetAllWithDetailsAsync();
    }
}
