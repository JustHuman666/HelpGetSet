using PL_API.Models;
using AutoMapper;
using BLL.EntitiesDto;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;

        /// <summary>
        /// Constructor for creating of controller with given services and mapper profile
        /// </summary>
        /// <param name="mapper">Auto mapper profile for models and dtos</param>
        /// <param name="messageService">Chat service</param>
        /// <param name="userProfileService">USer profile service</param>
        public MessageController(IMessageService messageService, IUserProfileService userProfileService, IMapper mapper)
        {
            _messageService = messageService;
            _userProfileService = userProfileService;
            _mapper = mapper;
        }

        /// <summary>
        /// To send a new message in chat
        /// </summary>
        /// <param name="messageModel">The instance of new message that should be sent</param>
        [HttpPost]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult> SendMessageInChat([FromBody] MessageModel messageModel)
        {
            messageModel.SenderId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _messageService.SendMessageInChatAsync(_mapper.Map<MessageDto>(messageModel));
            return Ok();
        }

        /// <summary>
        /// To delete chosen message for all users in the chat
        /// </summary>
        /// <param name="id">Id of message that should be deleted</param>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            await _messageService.DeleteMessageAsync(id);
            return Ok();
        }

        /// <summary>
        /// To get an instance of message by its id
        /// </summary>
        /// <param name="id">Id of message that is found</param>
        /// <returns>An instance of found message</returns>
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult<MessageModel>> GetMessageById(int id)
        {
            var message = await _messageService.GetMessageByIdAsync(id);
            return Ok(_mapper.Map<MessageModel>(message));
        }

        /// <summary>
        /// To get a collection of all messages
        /// </summary>
        /// <returns>Collection of messages</returns>
        [HttpGet]
        [Route("All")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult<IEnumerable<MessageModel>>> GetAllMessages()
        {
            var messages = await _messageService.GetAllMessagesAsync();
            return Ok(_mapper.Map<IEnumerable<MessageModel>>(messages));
        }

        /// <summary>
        /// To get a collection of all messages
        /// </summary>
        /// <returns>Collection of messages</returns>
        [HttpGet]
        [Route("MessagesOfChat/{id}")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult<IEnumerable<MessageModel>>> GetMessagesOfChat(int id)
        {
            var messages = await _messageService.GetAllMessagesAsync();
            var messagesOfChat = messages.Where(message => message.ChatId == id);
            return Ok(_mapper.Map<IEnumerable<MessageModel>>(messagesOfChat));
        }
    }
}
