using BLL.EntitiesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    /// <summary>
    /// Service interface for working with messages
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// To send a new message in chat
        /// </summary>
        /// <param name="item">The instance of new message that should be sent</param>
        Task SendMessageInChatAsync(MessageDto item);

        /// <summary>
        /// To delete chosen message for all users in the chat
        /// </summary>
        /// <param name="id">Id of message that should be deleted</param>
        Task DeleteMessageAsync(int id);

        /// <summary>
        /// To get an instance of message by its id
        /// </summary>
        /// <param name="id">Id of message that is found</param>
        /// <returns>An instance of found message</returns>
        Task<MessageDto> GetMessageByIdAsync(int id);

        /// <summary>
        /// To get a collection of all messages
        /// </summary>
        /// <returns>Queryable collection of messages</returns>
        Task<IEnumerable<MessageDto>> GetAllMessagesAsync();

        /// <summary>
        /// To get a collection of all messages in chosen chat
        /// </summary>
        /// <param name="id">The id of chat whish messages are finding</param>
        /// <returns>Collection of message/returns>
        Task<IEnumerable<MessageDto>> GetAllMessagesByChatIdAsync(int id);
    }
}
