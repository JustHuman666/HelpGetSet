using AutoMapper;
using BLL.EntitiesDto;
using BLL.Interfaces;
using BLL.Validation;
using DAL.Enteties;
using DAL.Interfaces.BaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    /// <summary>
    /// Service for working with messages in chats
    /// </summary>
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _db;

        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for creating of service with given unit of work (database and operations) and automapper profile
        /// </summary>
        /// <param name="database">Instance of unit of work for this service</param>
        /// <param name="mapper">Instance of automapper profile</param>
        public MessageService(IUnitOfWork database, IMapper mapper)
        {
            _db = database;
            _mapper = mapper;
        }

        public async Task DeleteMessageAsync(int id)
        {
            var message = await _db.Messages.GetByIdAsync(id);
            if (message == null)
            {
                throw new NotFoundException("Message does not exist");
            }
            _db.Messages.Delete(id);
            await _db.SaveAsync();
        }

        public async Task<IEnumerable<MessageDto>> GetAllMessagesAsync()
        {
            var messages = await _db.Messages.GetAllAsync();
            if (messages == null || messages.Count() == 0)
            {
                throw new NotFoundException("There is not any message");
            }
            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }

        public async Task<IEnumerable<MessageDto>> GetAllMessagesByChatIdAsync(int id)
        {
            var allMessages = await _db.Messages.GetAllAsync();
            if (allMessages == null || allMessages.Count() == 0)
            {
                throw new NotFoundException("There is not any message");
            }
            var chatMessages = allMessages.Where(message => message.ChatId == id);
            if (chatMessages == null || chatMessages.Count() == 0)
            {
                throw new NotFoundException("There is not any message in this chat");
            }
            return _mapper.Map<IEnumerable<MessageDto>>(chatMessages);
        }

        public async Task<MessageDto> GetMessageByIdAsync(int id)
        {
            var message = await _db.Messages.GetByIdAsync(id);
            if (message == null)
            {
                throw new NotFoundException("Message does not exist");
            }
            return _mapper.Map<MessageDto>(message);
        }

        public async Task SendMessageInChatAsync(MessageDto item)
        {
            if (item == null)
            {
                throw new HelpSiteException("Message cannot be null");
            }
            var message = await _db.Messages.GetByIdAsync(item.Id);
            if (message != null)
            {
                throw new HelpSiteException("Message already exist");
            }
            var chat = await _db.Chats.GetByIdAsync(item.ChatId);
            if (chat == null)
            {
                throw new NotFoundException("Chat does not exist");
            }
            var user = await _db.Users.GetByIdAsync(item.SenderId);
            if (user == null)
            {
                throw new NotFoundException("User does not exist");
            }
            if (string.IsNullOrEmpty(item.Text))
            {
                throw new HelpSiteException("Message text cannot be null or empty");
            }
            item.SendingTime = DateTime.Now;

            await _db.Messages.CreateAsync(_mapper.Map<Message>(item));
            await _db.SaveAsync();
        }
    }
}
