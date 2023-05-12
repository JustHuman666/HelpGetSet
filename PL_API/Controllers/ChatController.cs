using PL_API.Models;
using AutoMapper;
using BLL.EntitiesDto;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PL_API.Controllers
{
    /// <summary>
    /// Controller for working with chats
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for creating of controller with given services and mapper profile
        /// </summary>
        /// <param name="mapper">Auto mapper profile for models and dtos</param>
        /// <param name="chatService">Chat service</param>
        public ChatController(IChatService chatService, IMapper mapper)
        {
            _chatService = chatService;
            _mapper = mapper;
        }

        /// <summary>
        /// To create user chat with at least one user
        /// </summary>
        /// <param name="chatModel">Model of chat for creating with needed data</param>
        [HttpPost]
        [Route("CreateChat")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult> CreateNewChatForUser([FromBody] ChatModel chatModel)
        {
            var userIds = new HashSet<int>() { Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value) };
            if (chatModel.UserIds.Count() != 0)
            {
                foreach (var id in chatModel.UserIds)
                {
                    if (!userIds.Contains(id))
                    {
                        userIds.Add(id);
                    }
                }
            }

            chatModel.UserIds = userIds;
            await _chatService.CreateChatAsync(_mapper.Map<ChatDto>(chatModel));
            return Ok();
        }

        /// <summary>
        /// To get all chats of authorized user
        /// </summary>
        /// <returns>Collection of all chats of this user</returns>
        [HttpGet]
        [Route("AllUserChats")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult<IEnumerable<ChatDto>>> GetAllUsersChat()
        {
            var chats = await _chatService.GetAllChatsByUserIdAsync(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return Ok(chats);
        }

        /// <summary>
        /// To get all users of chosen chat
        /// </summary>
        /// <param name="id">Id of chat which users are finding</param>
        /// <returns>Collection of all users profiles in this chat</returns>
        [HttpGet]
        [Route("AllChatUsers/{id}")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult<IEnumerable<UserProfileModel>>> GetAllUsersInChat(int id)
        {
            var users = await _chatService.GetAllUsersOfChatAsync(id);
            return Ok(_mapper.Map<IEnumerable<UserProfileModel>>(users));
        }

        /// <summary>
        /// To rename chat by its id 
        /// </summary>
        /// <param name="id">The id of chat which name should be changed</param>
        /// <param name="name">The new name for chosen chat</param>
        [HttpPut]
        [Route("Rename/{id}/{name}")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult> RenameChat(int id, string name)
        {
            await _chatService.RenameChatAsync(id, name);
            return Ok();
        }

        /// <summary>
        /// To add new user in a chat
        /// </summary>
        /// <param name="chatId">The id of chat where user should be added</param>
        /// <param name="userId">The id of user who should be added</param>
        [HttpPost]
        [Route("AddUser/{chatId}/{userId}")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult> AddUserInChat(int chatId, int userId)
        {
            await _chatService.AddUserInChatAsync(chatId, userId);
            return Ok();
        }

        /// <summary>
        /// To delete chosen user from a chat
        /// </summary>
        /// <param name="chatId">The id of chat where user should be deleted</param>
        /// <param name="userId">The id of user who should be deleted</param>
        [HttpDelete]
        [Route("DeleteUser/{chatId}/{userId}")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult> DeleteUserFromChat(int chatId, int userId)
        {
            await _chatService.DeleteUserFromChatAsync(chatId, userId,
                Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return Ok();
        }

        /// <summary>
        /// To clear a history of messages of chosen chat
        /// /// </summary>
        /// <param name="id">The id of chat which history should be cleared</param>
        [HttpPut]
        [Route("ClearHistory/{id}")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult> ClearChatHistoryById(int id)
        {
            await _chatService.ClearChatHistoryByIdAsync(id,
                Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return Ok();
        }

        /// <summary>
        /// To delete chosen chat by its unique id
        /// </summary>
        /// <param name="id">Id of chat that should be deleted</param>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult> DeleteChat(int id)
        {
            await _chatService.DeleteChatAsync(id,
                Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return Ok();
        }

        /// <summary>
        /// To get chat with details by its id
        /// </summary>
        /// <param name="id">Id of chat that should be returned with details</param>
        /// <returns>Instance of chat with details</returns>
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult<IEnumerable<ChatDto>>> GetChatById(int id)
        {
            var chat = await _chatService.GetChatByIdWithDetailsAsync(id);
            return Ok(chat);
        }

        /// <summary>
        /// To change the status of chosen user in chosen chat for admin
        /// </summary>
        /// <param name="userId">The id of user who should be an admin</param>
        /// <param name="chatId">The id of chat where chosen user wanted to be admin</param>
        [HttpPut]
        [Route("MakeAdmin/{chatId}/{userId}")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult> SetAdminStatusToUser(int chatId, int userId)
        {
            await _chatService.SetAdminStatusToUserAsync(userId,
                Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                chatId);
            return Ok();
        }

        /// <summary>
        /// To change the status of chosen user in chosen chat for default
        /// </summary>
        /// <param name="userId">The id of chat that is finding</param>
        /// <param name="chatId">The id of chat where chosen user wanted to be admin</param>
        [HttpPut]
        [Route("MakeDefault/{chatId}/{userId}")]
        [Authorize(Roles = "Registered")]
        public async Task<ActionResult> SetDefaultStatusToUser(int chatId, int userId)
        {
            await _chatService.SetDefaultStatusToUserAsync(userId,
                Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                chatId);
            return Ok();
        }
    }
}
